using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SIMS_SE06206.Models
{
    public class StudentModel
    {
        public List<StudentViewModel> StudentsList { get; set; }
    }
    public class StudentViewModel
    {
        [Key]
        public string? Id { get; set; }
        [Required(ErrorMessage = "code can't be empty")]
        public string? code { get; set; }
        [Required(ErrorMessage ="first name can't be empty")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "last name can't be empty")]
        public string lastName { get; set; }
        
        [Required(ErrorMessage = "email can't be empty")]
        public string email { get; set; }
        [Required(ErrorMessage = "phone can't be empty")]
        public string phone { get; set; }
        [Required(ErrorMessage = "adress can't be empty")]
        public string address { get; set; }
        [AllowNull]
        public string? gender { get; set; }
        [AllowNull]
        public string birthday { get; set; }

    }
}
