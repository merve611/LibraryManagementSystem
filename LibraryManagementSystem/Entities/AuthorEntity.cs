namespace LibraryManagementSystem.Entities
{
    public class AuthorEntity       //Yazar
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }       
        public bool IsDeleted { get; set; }             //silinme işleminin olup olmadığını barındıran property

        public string ImageUrl { get; set; }


    }
}
