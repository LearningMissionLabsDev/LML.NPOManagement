namespace LML.NPOManagement.Common.Model
{
    public class UserInformationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RequestedUserRoleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public RequestedUserTypeEnum RequestedUserTypeEnum { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public Gender Gender { get; set; }
        public string? Metadata { get; set; }
        public virtual RequestedUserTypeModel RequestedUserRole { get; set; } = null!;
        public virtual UserModel User { get; set; } = null!;
    }
}
