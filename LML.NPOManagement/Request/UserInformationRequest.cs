using LML.NPOManagement.Bll.Model;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class UserInformationRequest
    {
        [Required]
        [Range(1, 3)]
        public UserTypeEnum UserTypeEnum { get; set; }


        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone()]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1, 3)]
        public Gender Gender { get; set; }

        [StringLength(int.MaxValue)]
        public string UserMetadata { get; set; }
    }
}
