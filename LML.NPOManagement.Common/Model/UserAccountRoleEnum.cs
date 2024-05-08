namespace LML.NPOManagement.Common
{
    [Flags]
    public enum UserAccountRoleEnum
    {
        SysAdmin = 1,
        Admin = 2,
        AccountManager = 4,
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
}
