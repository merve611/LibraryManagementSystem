using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class AuthorCreateViewModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
