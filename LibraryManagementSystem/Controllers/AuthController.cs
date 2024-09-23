using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private static List<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity{Id = 1, Email = ".", Password = "."}
        };
        private readonly IDataProtector _dataProtector;

        public AuthController(IDataProtectionProvider dataProtectionProvider)
        {
            //Bu robotu güvenlik amacıyla çağırdım
            _dataProtector = dataProtectionProvider.CreateProtector("security");

        }

        [HttpGet]
        public IActionResult SignUp()       //Kayıt olma
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var user = _users.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());
            if (user is not null)        //Bu kişi geldiyse yani user null değilse 
            {
                ViewBag.Error = "Kullanıcı Mevcut";
                return View(formData);

            }

            var newUser = new UserEntity()      //null ise yeni user oluşturulur
            {
                Id = _users.Max(x => x.Id) + 1,
                Email = formData.Email.ToLower(),
                Password = _dataProtector.Protect(formData.Password)        //_dataProtector robotunun Protect metoduna gönderdim formData dan gelen passwordü, Böylece şifrelenmiş şekilde listeye kaydedilir

            };
            _users.Add(newUser);

            return RedirectToAction("Index", "Book");


        }

        [HttpGet]
        public IActionResult SignLogin()        //Giriş yapma
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignLogin(SignLoginViewModel formData)
        {
            var user = _users.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());

            if(user is null)
            {
                ViewBag.Error = "Kullanıcı ve şifre hatalı";
                return View(formData);
            }

            var rawPassword = _dataProtector.Unprotect(user.Password);


            if(rawPassword == formData.Password)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("email", user.Email));
                claims.Add(new Claim("id", user.Id.ToString()));

                //Kimlik tanımlama
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //Claims içindeki verilerle bir oturum açılacağı için yukarıdaki identity nesnesi tanımlandı

                var autProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,        //yenilenebilir oturum 
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48))     //oturum açıldıktan sonra 48 saat geçerli



                };

                //Kimlik açma

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);
            }
            else
            {
                //şifre eşleşmezse;
                ViewBag.Error = "Kullanıcı ve şifre hatalı";
                return View(formData);
            }
            return RedirectToAction("Index", "Book");

        }
        public async Task<IActionResult> SignOut()      //çıkış yapma
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Book");

        }
    }
}
