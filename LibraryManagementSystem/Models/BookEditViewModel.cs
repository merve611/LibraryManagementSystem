using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class BookEditViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Kitap ismi bilgisini doldurmak zorunludur")]
        public string Title { get; set; }
        
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }

       
    }
}
