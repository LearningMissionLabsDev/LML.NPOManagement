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
}
