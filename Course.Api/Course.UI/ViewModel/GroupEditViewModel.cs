using System.ComponentModel.DataAnnotations;

namespace Course.UI.ViewModel
{
    public class GroupEditViewModel
    {
        [Required]
        [MaxLength(15)]
        [MinLength(5)]
        public string Name { get; set; }
    }
}
