using System.ComponentModel.DataAnnotations;

namespace Course.UI.ViewModel
{
    public class StudentCreateViewModel
    {
        [Required]
        [MaxLength(80)]
        public string FullName { get; set; }
        [Range(0, 100)]
        public decimal Point { get; set; }
        public int GroupId { get; set; }
    }
}
