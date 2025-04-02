using System.ComponentModel;
using System.Reflection;

namespace LML.NPOManagement.Common
{
    [Flags]
    public enum UserAccountRoleEnum
    {
        [Description("System Admin")]
        SysAdmin = 1,
        [Description("Admin")]
        Admin = 2,
        [Description("Account Manager")]
        AccountManager = 4,
        [Description("Beneficiary")]
        Beneficiary = 8
    }

    public static class RoleAccess
    {
        public const int SysAdminOnly = (int)UserAccountRoleEnum.SysAdmin;
        public const int AllAccess = (int)UserAccountRoleEnum.SysAdmin
            | (int)UserAccountRoleEnum.AccountManager
            | (int)UserAccountRoleEnum.Admin
            | (int)UserAccountRoleEnum.Beneficiary;

        public const int AccountAdmin = (int)UserAccountRoleEnum.SysAdmin
            | (int)UserAccountRoleEnum.Admin;

        public const int AdminsAndManager = (int)UserAccountRoleEnum.SysAdmin
            | (int)UserAccountRoleEnum.Admin
            | (int)UserAccountRoleEnum.AccountManager;

    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return value
                .GetType()
                .GetField(value.ToString())
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description ?? value.ToString();
        }
    }
}
