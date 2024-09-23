
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    //Kitapların detaylarını gösterip listelemede kullanılacak view model
    public class BookListViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap ismi bilgisini doldurmak zorunludur")]
        public string Title { get; set; }
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Kitap türü bilgisini doldurmak zorunludur")]
        [MinLength(3, ErrorMessage = "İçerik en az 3 karakter olmak zorundadır")]
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }

        public string AuthorName { get; set; }

    }
}
