using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        static List<AuthorEntity> _authors = new List<AuthorEntity>()
        {
            new AuthorEntity { Id = 1, FirstName = "Sabahattin", LastName= "Ali", ImageUrl = "../images/Sabahattin_ali.jpg" },
            new AuthorEntity { Id = 2, FirstName = "J. K. ", LastName= "Rowling", ImageUrl = "../images/J.K.-Rowling.jpg"},
            new AuthorEntity { Id = 3, FirstName = "Fyodor", LastName="Dostoyevski", ImageUrl = "../images/Dostoyevski.jpg"},
            new AuthorEntity { Id = 4, FirstName = "Pierre ", LastName= "Franckh", ImageUrl = "../images/Pierre.jpg"},
            new AuthorEntity { Id = 5, FirstName = "İlber", LastName= "Ortaylı", ImageUrl = "../images/ilberuortay.jpg"},


        };
        static List<BookEntity> _books = new List<BookEntity>()
        {
            new BookEntity{ Id = 1, Title = "Sırça Köşk", AuthorId = 1, Genre = "Psikolojik Roman", PublishDate =  new DateTime(1947, 1, 1)},

            new BookEntity{ Id = 2, Title = "Harry Potter ve Felsefe Taşı", AuthorId = 2, Genre = "Fantastik Roman", PublishDate = new DateTime(1997,1, 1) },
            new BookEntity{ Id = 3, Title = "Suç ve Ceza", AuthorId = 3, Genre = "Felsefi Roman", PublishDate = new DateTime(1886, 1, 1) },
            new BookEntity{ Id = 4, Title = "Rezonans Kanunu", AuthorId = 4, Genre = "Kişisel Gelişim", PublishDate = new DateTime(2019,1, 1) },
            new BookEntity{ Id = 5, Title = "Bir Ömür Nasıl Yaşanır", AuthorId = 5, Genre = "Kişisel Gelişim", PublishDate = new DateTime(2019, 1, 1) },
            new BookEntity{ Id = 5, Title = "Bir Ömür Nasıl Yaşanırrrrrrrrrrrrrrr", AuthorId = 6, Genre = "Kişisel Gelişim", PublishDate = new DateTime(2019, 1, 1) },
        };

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()             //Yazarları listelerken kullanılan action
        {
            var viewModel = _authors.Where(x => x.IsDeleted == false).Select(x => new AuthorListViewModel           //AuthorListViewModel modeli kullanılarak listeleme işlemi yapılır
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImageUrl = x.ImageUrl,


            }).ToList();
            return View(viewModel);
        }

        public IActionResult Details(int id)            //Detayda book ve author tabloları birleştirilerek artık author tarafında da book nesneleri view'e taşınabilir
        {
            var viewModel = _authors.Where(x => x.Id == id).Select(x => new AuthorListViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImageUrl = x.ImageUrl,
            }).FirstOrDefault();

            if (viewModel is null)
            {
                return NotFound();
            }


            return View(viewModel);


        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _authors;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateViewModel model)
        {
            if (!ModelState.IsValid)             //Modele uymuyorsa yine aynısını geri döndür
            {
                return View();
            }
            int maxId = _authors.Max(x => x.Id);

            // Yazar oluşturulurken resim dosyası eklenebilmesi için
            // Eğer dosya yüklendiyse
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                // Dosya adını alma
                string fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                string extension = Path.GetExtension(model.ImageUrl.FileName);
                fileName = fileName + extension;

                // Dosyanın sunucuda kaydedileceği yol
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                // Eğer dizin yoksa oluştur
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Tam dosya yolu
                string filePath = Path.Combine(uploadPath, fileName);

                // Dosyayı kaydetme
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageUrl.CopyToAsync(fileStream);
                }

                // Burada model.ImageFile yerine dosyanın sunucuda kaydedildiği `filePath`'i saklayabilir veya veritabanına kaydedebilirsiniz.
            }


            var newAuthor = new AuthorEntity()
            {
                Id = maxId + 1,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = "../images/" + model.ImageUrl.FileName
                
                
            };
            _authors.Add(newAuthor);

           
            return RedirectToAction("List");
            // Eğer model geçersizse formu yeniden göster
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = _authors.Find(x=> x.Id == id);

            var viewModel = new AuthorEditViewModel()
            {

               Id = author.Id,
               FirstName = author.FirstName,
               LastName = author.LastName
              

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorEditViewModel formData)
        {
            if (!ModelState.IsValid)
            {                
                return View(formData);
            }

            if (formData.ImageUrl != null && formData.ImageUrl.Length > 0)
            {
                // Dosya adını alma
                string fileName = Path.GetFileNameWithoutExtension(formData.ImageUrl.FileName);
                string extension = Path.GetExtension(formData.ImageUrl.FileName);
                fileName = fileName + extension;

                // Dosyanın sunucuda kaydedileceği yol
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                // Eğer dizin yoksa oluştur
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Tam dosya yolu
                string filePath = Path.Combine(uploadPath, fileName);

                // Dosyayı kaydetme
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formData.ImageUrl.CopyToAsync(fileStream);
                }

                // Burada model.ImageFile yerine dosyanın sunucuda kaydedildiği `filePath`'i saklayabilir veya veritabanına kaydedebilirsiniz.
            }

            var author = _authors.Find(x => x.Id == formData.Id);

            author.FirstName = formData.FirstName;
            author.LastName = formData.LastName;
            author.ImageUrl = "../images/" + formData.ImageUrl.FileName;



            return RedirectToAction("List");

        }


        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var author = _authors.Find(x => x.Id == id);
            if (author != null)
            {
                ViewBag.AuthorTitle = author.FirstName +" "+ author.LastName; // Kitap başlığını view'a gönderiyoruz
            }

            var viewModel = new AuthorDeleteViewModel
            {
                AuthorId = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                
            };

            return View("Delete", viewModel);               //Id yi tutabilmek için model de gönderildi

        }


        // Silme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var author = _authors.Find(x => x.Id == id);
            if (author != null)
            {
                author.IsDeleted = true;
                TempData["DeleteMessage"] = $"{author.FirstName + author.LastName} başarıyla silindi.";
            }
            return RedirectToAction("List");
        }




    }

}

