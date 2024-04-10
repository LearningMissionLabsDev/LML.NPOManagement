namespace LML.NPOManagement.Common
{
    [Flags]
    public enum UserAccountRoleEnum
    {
        Admin = 1 << 0,
        AccountManager = 1 << 1,
        Beneficiary = Admin | AccountManager
    }
}
