
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        static List<BookEntity> _books = new List<BookEntity>()
        {
            new BookEntity{ Id = 1, Title = "Sırça Köşk", AuthorId = 1, Genre = "Psikolojik Roman", PublishDate =  new DateTime(1947, 1, 1)},

            new BookEntity{ Id = 2, Title = "Harry Potter ve Felsefe Taşı", AuthorId = 2, Genre = "Fantastik Roman", PublishDate = new DateTime(1997,1, 1) },
            new BookEntity{ Id = 3, Title = "Suç ve Ceza", AuthorId = 3, Genre = "Felsefi Roman", PublishDate = new DateTime(1886, 1, 1) },
            new BookEntity{ Id = 4, Title = "Rezonans Kanunu", AuthorId = 4, Genre = "Kişisel Gelişim", PublishDate = new DateTime(2019,1, 1) },
            new BookEntity{ Id = 5, Title = "Bir Ömür Nasıl Yaşanır", AuthorId = 5, Genre = "Kişisel Gelişim", PublishDate = new DateTime(2019, 1, 1) },
        };

        static List<AuthorEntity> _authors = new List<AuthorEntity>()
        {
            new AuthorEntity { Id = 1, FirstName = "Sabahattin", LastName= "Ali", ImageUrl = "../images/Sabahattin_ali.jpg" },
            new AuthorEntity { Id = 2, FirstName = "J. K. ", LastName= "Rowling", ImageUrl = "../images/J.K.-Rowling.jpg"},
            new AuthorEntity { Id = 3, FirstName = "Fyodor", LastName="Dostoyevski", ImageUrl = "../images/Dostoyevski.jpg"},
            new AuthorEntity { Id = 4, FirstName = "Pierre ", LastName= "Franckh", ImageUrl = "../images/Pierre.jpg"},
            new AuthorEntity { Id = 5, FirstName = "İlber", LastName= "Ortaylı", ImageUrl = "../images/ilberuortay.jpg"}
        };



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()             //Kitapları listeleme actionı
        {
            var viewModel = _books.Where(x => x.IsDeleted == false)
                .Join(
                _authors,
                book => book.AuthorId,
                author => author.Id,
                (book, author) => new { book, author }).Select(x => new BookListViewModel       //silinmemiş kitapları seçerek ilgili viewmodele projekte ederek listelenir
                {
                    Id = x.book.Id,
                    Title = x.book.Title,
                    AuthorName = x.author.FirstName + " " + x.author.LastName,
                    Genre = x.book.Genre,
                    PublishDate = x.book.PublishDate
                }).ToList();


            return View(viewModel);
        }
        public IActionResult Details(int id)        //Kitap detayını gösteren action
        {
            var viewModel = _books.Where(x => x.Id == id)
                .Join(
                _authors,
                book => book.AuthorId,
                author => author.Id,
                (book, author) => new { book, author }).Select(x => new BookListViewModel       //silinmemiş kitapları seçerek ilgili viewmodele projekte ederek listelenir
                {
                    Id = x.book.Id,
                    Title = x.book.Title,
                    AuthorName = x.author.FirstName + " " + x.author.LastName,
                    Genre = x.book.Genre,
                    PublishDate = x.book.PublishDate
                }).FirstOrDefault();                //Tek bir eleman geleceği için



            if (viewModel is null)
            {
                return NotFound();
            }



            return View(viewModel);

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _authors.Select(a => new              //veriyi view'e taşırken isim ve soyismi fullname değişkeni altında taşıması için
            {
                Id = a.Id,
                FullName = $"{a.FirstName} {a.LastName}"
            });

            return View();
        }

        [HttpPost]
        public IActionResult Create(BookCreateViewModel formData)         //Kitap oluşturma actionı
        {
            if (!ModelState.IsValid)             //Modele uymuyorsa yine aynısını geri döndür
            {
                return View();
            }
            int maxId = _books.Max(x => x.Id);

            var newBook = new BookEntity()
            {
                Id = maxId + 1,
                Title = formData.Title,
                AuthorId = formData.AuthorId,
                Genre = formData.Genre,
                PublishDate = formData.PublishDate,
            };
            _books.Add(newBook);


            return RedirectToAction("List");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _books.Find(x => x.Id == id);

            var viewModel = new BookEditViewModel()
            {
                
                Title = book.Title,
                
                Genre = book.Genre,
                PublishDate = book.PublishDate,
            };

            ViewBag.Authors = _authors;

            return View(viewModel);

        }

        [HttpPost]
        public IActionResult Edit(BookEditViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var book = _books.Find(x => x.Id == formData.Id);

            book.Title = formData.Title;
            book.Genre = formData.Genre;
            book.PublishDate = formData.PublishDate;



            return RedirectToAction("List");

        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var book = _books.Find(x => x.Id == id);
            if (book != null)
            {
                ViewBag.BookTitle = book.Title; // Kitap başlığını view'a gönderiyoruz
            }

            var viewModel = new BookDeleteViewModel
            {
                BookId = book.Id,
                Title = book.Title,
            };

            return View("Delete", viewModel);               //Id yi tutabilmek için model de gönderildi
         
        }
        

        // Silme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var book = _books.Find(x => x.Id == id);
            if (book != null)
            {
                book.IsDeleted = true;
                TempData["DeleteMessage"] = $"{book.Title} başarıyla silindi.";
            }
            return RedirectToAction("List");
        }


    }
}
