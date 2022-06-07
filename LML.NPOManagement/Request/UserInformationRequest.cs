using LML.NPOManagement.Bll.Model;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class UserInformationRequest
    {
        [Required]
<<<<<<< HEAD
        [Range(1, 3)]
        public UserTypeEnum UserTypeEnum { get; set; }


=======
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

>>>>>>> 602de833836cd76818c179edc83fbe911dbd9653
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
<<<<<<< HEAD
=======
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }

        [Required]
>>>>>>> 602de833836cd76818c179edc83fbe911dbd9653
        [Phone()]
        public string PhoneNumber { get; set; }

        [Required]
<<<<<<< HEAD
        [Range(1, 3)]
        public Gender Gender { get; set; }

        [StringLength(int.MaxValue)]
=======
        public Gender Gender { get; set; }


        [StringLength (int.MaxValue)]
>>>>>>> 602de833836cd76818c179edc83fbe911dbd9653
        public string UserMetadata { get; set; }
    }
}
