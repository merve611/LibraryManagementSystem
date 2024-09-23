using LibraryManagementSystem.Entities;

namespace LibraryManagementSystem.Models
{
    public static class StaticClass
    {
        public static List<AuthorEntity> _authors = new List<AuthorEntity>()
        {
            new AuthorEntity { Id = 1, FirstName = "Sabahattin", LastName= "Ali", ImageUrl = "../images/Sabahattin_ali.jpg" },
            new AuthorEntity { Id = 2, FirstName = "J. K. ", LastName= "Rowling", ImageUrl = "../images/J.K.-Rowling.jpg"},
            new AuthorEntity { Id = 3, FirstName = "Fyodor", LastName="Dostoyevski", ImageUrl = "../images/Dostoyevski.jpg"},
            new AuthorEntity { Id = 4, FirstName = "Pierre ", LastName= "Franckh", ImageUrl = "../images/Pierre.jpg"},
            new AuthorEntity { Id = 5, FirstName = "İlber", LastName= "Ortaylı", ImageUrl = "../images/ilberuortay.jpg"},


        };
        public static List<BookEntity> _books = new List<BookEntity>()
        {
            new BookEntity{ Id = 1, Title = "Sırça Köşk", AuthorId = 1, Genre = "Psikolojik Roman", PublishDate =  new DateTime(1947, 1, 1)},

            new BookEntity{ Id = 2, Title = "Harry Potter ve Felsefe Taşı", AuthorId = 2, Genre = "Fantastik Roman", PublishDate = new DateTime(1997,1, 1) },
            new BookEntity{ Id = 3, Title = "Suç ve Ceza", AuthorId = 3, Genre = "Felsefi Roman", PublishDate = new DateTime(1886, 1, 1) },
            new BookEntity{ Id = 4, Title = "Rezonans Kanunu", AuthorId = 4, Genre = "Kişisel Gelişim", PublishDate = new DateTime(2019,1, 1) },
            new BookEntity{ Id = 5, Title = "Bir Ömür Nasıl Yaşanır", AuthorId = 5, Genre = "Kişisel Gelişim", PublishDate = new DateTime(2019, 1, 1) }
        };
    }
}
