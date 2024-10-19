using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Reflection;
using System.Text;

namespace LML.ApiSpecGenerator
{
    public struct MethodToTest
    {
        public string MethodName { get; set; }
        public string MethodRole { get; set; }

        public MethodToTest(string methodName, string methodRole)
        {
            MethodName = methodName;
            MethodRole = methodRole;
        }
    }

    public struct TestedMethod
    {
        public string MethodName { get; set; }
        public string MethodRole { get; set; }
        public List<bool> IsAllowed { get; set; }

        public TestedMethod(string methodName, string methodRole, List<bool> allowed)
        {
            MethodName = methodName;
            MethodRole = methodRole;
            IsAllowed = allowed;
        }
    }

    public class AuthorizationHeaderRolesTestUtils
    {
        private readonly string? BASE_DIRECTORY = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        private readonly List<TestedMethod> testingMethodsDetails = new();

        private readonly List<int> roles = new() {
                    (int)UserAccountRoleEnum.SysAdmin,
                    (int)UserAccountRoleEnum.Admin,
                    (int)UserAccountRoleEnum.AccountManager,
                    (int)UserAccountRoleEnum.Beneficiary
            };

        public bool TestControllerAuthorizationRoles<T>()
        {
            CreateDirectory();
            string filePathToReadDetails = Path.Combine(BASE_DIRECTORY + "/ApiSpecification/", typeof(T).Name + ".csv");
            string filePathToWriteResults = Path.Combine(BASE_DIRECTORY + "/Output/", typeof(T).Name + "Results.csv");

            var testAuthorizationRoles = TestAuthorizationRoles(filePathToReadDetails, filePathToWriteResults);
            return testAuthorizationRoles;
        }

        private static List<MethodToTest> InspectControllerAuthorization<T>()
        {
            var controllerType = typeof(T);
            var methods = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            var testingMethods = new List<MethodToTest>();

            foreach (var method in methods)
            {
                var authorizeAttributes = method.GetCustomAttributes<AuthorizeAttribute>();
                foreach (var authorizeAttribute in authorizeAttributes)
                {
                    var roleField = typeof(AuthorizeAttribute).GetField("_role", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (roleField != null)
                    {
                        var roleValue = roleField.GetValue(authorizeAttribute);
                        var methodForTesting = new MethodToTest(method.Name, ConvertIntToRoleAccess(Convert.ToInt16(roleValue)));
                        testingMethods.Add(methodForTesting);
                    }
                }
            }

            return testingMethods;
        }

        private bool TestAuthorizationRoles(string csvFilePathToRead, string csvFilePathToWrite)
        {
            List<TestedMethod> testedMethodsResult = new();
            var testingMethods = ReadMethodDetailsFromFile(csvFilePathToRead);

            foreach (var method in testingMethods)
            {
                var currentMethodRole = GetIntFromRoleString(method.MethodRole);
                var results = new List<bool>();

                foreach (var role in roles)
                {
                    var context = role switch
                    {
                        (int)UserAccountRoleEnum.SysAdmin => SysAdminAuthorize(currentMethodRole),
                        (int)UserAccountRoleEnum.Admin => AdminAuthorize(currentMethodRole),
                        (int)UserAccountRoleEnum.AccountManager => AccountManagerAuthorize(currentMethodRole),
                        (int)UserAccountRoleEnum.Beneficiary => BeneficiaryAuthorize(currentMethodRole),
                        _ => throw new InvalidOperationException("Unknown role")
                    };

                    bool isAllowed = IsAllowed(context);
                    results.Add(isAllowed);
                }

                var testedMethod = new TestedMethod(method.MethodName, method.MethodRole, results);
                testedMethodsResult.Add(testedMethod);
            }

            return WriteToFileTestResults(csvFilePathToWrite, testedMethodsResult);
        }

        private static bool IsAllowed(AuthorizationFilterContext context)
        {
            if (context.Result == null)
            {
                return true;
            }

            return context.Result is JsonResult jsonResult && !(jsonResult.Value.ToString().ToLower().Contains("denied"));
        }

        private static AuthorizationFilterContext SysAdminAuthorize(int role)
        {
            var user = new UserModel();
            var attribute = new AuthorizeAttribute(role);
            var account = new Account2UserModel { AccountRoleId = (int)UserAccountRoleEnum.SysAdmin };

            var context = GetMockedContext(user, account);
            attribute.OnAuthorization(context);
            return context;
        }

        private static AuthorizationFilterContext AdminAuthorize(int role)
        {
            var user = new UserModel();
            var attribute = new AuthorizeAttribute(role);
            var account = new Account2UserModel { AccountRoleId = (int)UserAccountRoleEnum.Admin };

            var context = GetMockedContext(user, account);
            attribute.OnAuthorization(context);
            return context;
        }

        private static AuthorizationFilterContext AccountManagerAuthorize(int role)
        {
            var user = new UserModel();
            var attribute = new AuthorizeAttribute(role);
            var account = new Account2UserModel { AccountRoleId = (int)UserAccountRoleEnum.AccountManager };

            var context = GetMockedContext(user, account);
            attribute.OnAuthorization(context);
            return context;
        }

        private static AuthorizationFilterContext BeneficiaryAuthorize(int role)
        {
            var user = new UserModel();
            var attribute = new AuthorizeAttribute(role);
            var account = new Account2UserModel { AccountRoleId = (int)UserAccountRoleEnum.Beneficiary };

            var context = GetMockedContext(user, account);
            attribute.OnAuthorization(context);
            return context;
        }

        private static AuthorizationFilterContext GetMockedContext(UserModel user, Account2UserModel account)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items["User"] = user;
            httpContext.Items["Account"] = account;
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext,
                new RouteData(),
                new ActionDescriptor()),
                new List<IFilterMetadata>());
            return context;
        }


        private List<MethodToTest> ReadMethodDetailsFromFile(string filePath)
        {
            var methodsList = new List<MethodToTest>();
            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;
                    var values = line.Split(',');
                    if (values.Length >= 2)
                    {
                        var methodForTesting = new MethodToTest(values[0], values[1]);
                        methodsList.Add(methodForTesting);
                        testingMethodsDetails.Add(new TestedMethod(values[0], values[1], new List<bool>
                        {
                            values[2] == "Allowed",
                            values[3] == "Allowed",
                            values[4] == "Allowed",
                            values[5] == "Allowed"
                        }));
                    }
                }
            }
            return methodsList;
        }

        private bool WriteToFileTestResults(string filePath, List<TestedMethod> testedMethods)
        {
            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.WriteLine("Method Name,Role Access," +
                "Test: SysAdmin,Test: Admin,Test: AccoountManager,Test: Beneficiary,Test Result");

            var isTotalFailed = false;
            for (int i = 0; i < testedMethods.Count; ++i)
            {
                var method = testedMethods[i];

                var sysAdminTest = method.IsAllowed[0] == testingMethodsDetails[i].IsAllowed[0] ? "Passed" : "Failed";
                var adminTest = method.IsAllowed[1] == testingMethodsDetails[i].IsAllowed[1] ? "Passed" : "Failed";
                var accoountManagerTest = method.IsAllowed[2] == testingMethodsDetails[i].IsAllowed[2] ? "Passed" : "Failed";
                var beneficiaryTest = method.IsAllowed[3] == testingMethodsDetails[i].IsAllowed[3] ? "Passed" : "Failed";

                var isCurrentTestFailed = (sysAdminTest + adminTest + accoountManagerTest + beneficiaryTest).Contains("Failed");
                var currentTestStatus = isCurrentTestFailed ? "Failed" : "Passed";

                if (isCurrentTestFailed) isTotalFailed = true;

                writer.WriteLine($"{method.MethodName},{method.MethodRole}," +
                    $"{sysAdminTest},{adminTest},{accoountManagerTest},{beneficiaryTest},{currentTestStatus}");
            }
            var totalStatus = isTotalFailed ? "Failed" : "Passed";
            writer.WriteLine($",,,,,,{totalStatus}");
            return !isTotalFailed;
        }

        private void WriteMethodDetailsInFile(string filePath, List<MethodToTest> methods)
        {
            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.WriteLine("Method Name,Role Access,System Admin,Account Admin,Account Manager,Beneficiary");
            foreach (var method in methods)
            {
                string res = IsAllowedForRole(GetIntFromRoleString(method.MethodRole));
                var roleAccessDetails = res.Split(',');
                if (roleAccessDetails.Length == 5)
                {
                    writer.WriteLine($"{method.MethodName},{method.MethodRole},{roleAccessDetails[0]},{roleAccessDetails[1]},{roleAccessDetails[2]},{roleAccessDetails[3]}");
                }
                else
                {
                    writer.WriteLine($"{method.MethodName},{method.MethodRole},,,,");
                }
            }
        }

        private string IsAllowedForRole(int role)
        {
            var res = "";
            for (int i = 0; i < roles.Count; ++i)
            {
                if ((roles[i] & role) == roles[i])
                {
                    res += "Allowed,";
                }
                else
                {
                    res += "Denied,";
                }
            }
            return res;
        }

        private int GetIntFromRoleString(string constantName)
        {
            var type = typeof(RoleAccess);
            var field = type.GetField(constantName, BindingFlags.Public | BindingFlags.Static);

            if (field != null && field.FieldType == typeof(int))
            {
                var value = field.GetValue(null);
                if (value != null)
                {
                    return (int)value;
                }
                else
                {
                    throw new InvalidOperationException($"The value of constant '{constantName}' is null in {nameof(RoleAccess)}");
                }
            }
            else
            {
                throw new ArgumentException($"Constant '{constantName}' not found or not an integer in {nameof(RoleAccess)}");
            }
        }

        private static string ConvertIntToRoleAccess(int value)
        {
            return value switch
            {
                RoleAccess.SysAdminOnly => nameof(RoleAccess.SysAdminOnly),
                RoleAccess.AllAccess => nameof(RoleAccess.AllAccess),
                RoleAccess.AccountAdmin => nameof(RoleAccess.AccountAdmin),
                RoleAccess.AdminsAndManager => nameof(RoleAccess.AdminsAndManager),
                _ => "Unknown RoleAccess"
            };
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(BASE_DIRECTORY + "/Output/"))
            {
                Directory.CreateDirectory(BASE_DIRECTORY + "/Output/");
            }
        }

        public bool IsProvided<T>()
        {
            string file = BASE_DIRECTORY + "/ApiSpecification/" + typeof(T).Name + ".csv";
            return File.Exists(file) && CheckAPISpecification<T>(file);
        }

        private bool CheckAPISpecification<T>(string file)
        {
            var methods = InspectControllerAuthorization<T>();
            var specializedMethods = ReadMethodDetailsFromFile(file);

            if (methods.Count > specializedMethods.Count)
            {
                return false;
            }

            foreach (var method in methods)
            {
                if (!specializedMethods.Contains(method))
                {
                    return false;
                }
            }

            return true;
        }

        public static void Main(string[] args)
        {
            /*
                // Example of Testing Authorization Header in AccountController

                // Create an instance of the test utility class to handle authorization header roles.
                var exampleUtils = new AuthorizationHeaderRolesTestUtils();

                // Check if all necessary files (e.g., roles, function names) have been provided by the product manager.
                var isProvided = exampleUtils.IsProvided<AccountController>(); 

                // If the necessary files have been provided, test the authorization roles for the specified controller type.
                if (isProvided)
                {
                    exampleUtils.TestControllerAuthorizationRoles<AccountController>();
                }
                    
                If you want to generate a specification file based on the code, follow this example:

                // Example usage:
                // Uncomment the code below if you want to inspect the authorization of the controller
                // and write the details to a specification file.

                var exampleUtils = new AuthorizationHeaderRolesTestUtils();

                // Inspect the controller's authorization methods. Replace <T> with your specific controller type.
                var testingMethods = InspectControllerAuthorization<T>();  

                // Define the path where the specification file will be written.
                string filePathToWriteSpecifications = Path.Combine(exampleUtils.BASE_DIRECTORY + "/ApiSpecification/", typeof(T).Name + ".csv");

                // Write the method details to the specified file.
                exampleUtils.WriteMethodDetailsInFile(filePathToWriteSpecifications, testingMethods);
            */
        }
    }
}