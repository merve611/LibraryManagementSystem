namespace LibraryManagementSystem.Models
{
    public class AuthorEditViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
