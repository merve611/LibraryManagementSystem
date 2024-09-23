namespace LibraryManagementSystem.Models
{
    //Yazarların detaylarını gösterip listelemede kullanılacak view model
    public class AuthorListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    
    }
}
