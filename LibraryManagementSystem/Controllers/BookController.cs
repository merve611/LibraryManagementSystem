
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()             //Kitapları listeleme actionı
        {
            var viewModel = StaticClass._books.Where(x => x.IsDeleted == false)
                .Join(
                StaticClass._authors,
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
            var viewModel = StaticClass._books.Where(x => x.Id == id)
                .Join(
                StaticClass._authors,
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
            ViewBag.Authors = StaticClass._authors.Select(a => new              //veriyi view'e taşırken isim ve soyismi fullname değişkeni altında taşıması için
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
            int maxId = StaticClass._books.Max(x => x.Id);

            var newBook = new BookEntity()
            {
                Id = maxId + 1,
                Title = formData.Title,
                AuthorId = formData.AuthorId,
                Genre = formData.Genre,
                PublishDate = formData.PublishDate,
            };
            StaticClass._books.Add(newBook);


            return RedirectToAction("List");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = StaticClass._books.Find(x => x.Id == id);

            var viewModel = new BookEditViewModel()
            {
                
                Title = book.Title,
                
                Genre = book.Genre,
                PublishDate = book.PublishDate,
            };

            ViewBag.Authors = StaticClass._authors;

            return View(viewModel);

        }

        [HttpPost]
        public IActionResult Edit(BookEditViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var book = StaticClass._books.Find(x => x.Id == formData.Id);

            book.Title = formData.Title;
            book.Genre = formData.Genre;
            book.PublishDate = formData.PublishDate;



            return RedirectToAction("List");

        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var book = StaticClass._books.Find(x => x.Id == id);
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
            var book = StaticClass._books.Find(x => x.Id == id);
            if (book != null)
            {
                book.IsDeleted = true;
                TempData["DeleteMessage"] = $"{book.Title} başarıyla silindi.";
            }
            return RedirectToAction("List");
        }


    }
}
