
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using B201200001_WEB;
using B201200001_WEB.Models;

namespace B201200001_WEB.Controllers
{
    public class HomeController : Controller
    {

        web_programlamaEntities _context = new web_programlamaEntities();
        // GET: Home
        public ActionResult Home()
        {
            return View(_context.Oyuncak.ToList());
        }





        public ActionResult Details(int id)
        {
            // ID'yi kullanarak gerekli işlemleri gerçekleştirin
            // Örneğin, ID'yi kullanarak modeli veritabanından çekmek gibi...
            var product = _context.Oyuncak.Find(id);



            // Modeli sayfaya gönderin
            return View("Details", product);

        }

        public ActionResult About()
        {


            return View();
        }

       
        public ActionResult Login()
        {


            return View();
        }


        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            var user = _context.Kullanici.FirstOrDefault(u => u.Email == email);
            if (user != null && user.Sifre == password)
            {

                Session["Kullanici_ID"] = user.Kullanici_ID;

                return RedirectToAction("Home", "Home");
            }

            else
            {
                ViewBag.ErrorMessage = "Invalid email or password";
                ViewBag.CurrentUser = null;
                return View();
            }

        }


        public ActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string name, string email, string password, string confirmPassword)
        {
            // Şifre doğrulaması yap
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Password and Confirm Password do not match";
                return View("SignUp");
            }

            // E-posta adresiyle daha önce kayıtlı bir kullanıcı var mı kontrol et
            var existingUser = _context.Kullanici.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Email address is already registered";
                return View("SignUp");
            }

            var newAdres = new Adres();

            // Eğer yeni kullanıcı için bir adres bilgisi girilmişse, bu bilgileri kullan
            newAdres.Sehir_ID = 2; // Burada varsayılan bir şehir ID'si verdik, gerçek verileri kullanmalısınız
            newAdres.ilce_ID = 1; // Burada varsayılan bir ilçe ID'si verdik, gerçek verileri kullanmalısınız
            newAdres.Mahalle_ID = 1; // Burada varsayılan bir mahalle ID'si verdik, gerçek verileri kullanmalısınız
            newAdres.Sokak_ID = 1; // Burada varsayılan bir sokak ID'si verdik, gerçek verileri kullanmalısınız
            newAdres.Bina_No = 5; // Burada varsayılan bir bina numarası verdik, gerçek verileri kullanmalısınız

            // Yeni kullanıcı oluştur ve veritabanına ekle
            var newUser = new Kullanici { Kullanici_Ad = name, Email = email, Sifre = password, Adres_ID = newAdres.Adres_ID, Admin_Situation = false };

            _context.Kullanici.Add(newUser);
            _context.SaveChanges();

            // Kullanıcıyı giriş sayfasına yönlendir
            return RedirectToAction("Login");
        }





        public ActionResult SettingsPage()
        {



            int userId = (int)Session["Kullanici_ID"];
            var user = _context.Kullanici.Find(userId);

            // Kullanıcının adres bilgilerini al
            var adres = _context.Adres.FirstOrDefault(a => a.Adres_ID == user.Adres_ID);

            AdresViewModel adresViewModel;

            if (adres != null)
            {
                adresViewModel = new AdresViewModel

                {
                    Sehir = _context.Sehir.FirstOrDefault(s => s.Sehir_ID == adres.Sehir_ID)?.Sehir_Ad,
                    Ilce = _context.ilce.FirstOrDefault(i => i.ilce_ID == adres.ilce_ID)?.ilce_Ad,
                    Mahalle = _context.Mahalle.FirstOrDefault(m => m.Mahalle_ID == adres.Mahalle_ID)?.Mahalle_Ad,
                    Sokak = _context.Sokak.FirstOrDefault(s => s.Sokak_ID == adres.Sokak_ID)?.Sokak_Ad,
                    BinaNo = adres.Bina_No
                };
            }


            else
            {
                // Eğer kullanıcının adresi yoksa, SignUp işleminden alınan varsayılan değerleri kullan
                var newAdres = new Adres();
                adresViewModel = new AdresViewModel
                {
                    Sehir = _context.Sehir.FirstOrDefault(s => s.Sehir_ID == newAdres.Sehir_ID)?.Sehir_Ad,
                    Ilce = _context.ilce.FirstOrDefault(i => i.ilce_ID == newAdres.ilce_ID)?.ilce_Ad,
                    Mahalle = _context.Mahalle.FirstOrDefault(m => m.Mahalle_ID == newAdres.Mahalle_ID)?.Mahalle_Ad,
                    Sokak = _context.Sokak.FirstOrDefault(s => s.Sokak_ID == newAdres.Sokak_ID)?.Sokak_Ad,
                    BinaNo = newAdres.Bina_No
                };
            }


            //else
            //{
            //    adresViewModel = new AdresViewModel
            //    // Eğer kullanıcının adresi yoksa varsayılan bir değer atayabilir veya adresViewModel'i null bırakabilirsiniz.

            //    {
            //        Sehir = "İstanbul",
            //        Ilce = "Bilinmiyor",
            //        Mahalle = "Bilinmiyor",
            //        Sokak = "Bilinmiyor",
            //        BinaNo = null
            //    };
            //}


            var cities = _context.Sehir.ToList();
            var ilceler = _context.ilce.ToList();
            var mahalleler = _context.Mahalle.ToList();
            var sokaklar = _context.Sokak.ToList();


            var SettingsModel = new SettingsViewModel
            {
                User = user,
                Adres = adresViewModel,
                Cities = cities,
                Ilceler = ilceler,
                Mahalleler = mahalleler,
                Sokaklar = sokaklar

            };


            // Modeli sayfaya gönderin
            return View("SettingsPage", SettingsModel);

        }


        public ActionResult CreateAdress()
        {
            return View();
        }




        [HttpPost]
        public ActionResult SettingsPage(string selectedCity, string selectedIlce, string selectedMahalle, string selectedSokak, string BinaNo)
        {
            try
            {
                int userId = (int)Session["Kullanici_ID"];
                var user = _context.Kullanici.Find(userId);
                var existingAdres = _context.Adres.FirstOrDefault(a => a.Adres_ID == user.Adres_ID);

                if (existingAdres == null)
                {
                    existingAdres = new Adres();
                    _context.Adres.Add(existingAdres);
                }

                // Yeni adres bilgilerini güncelle
                existingAdres.Sehir_ID = _context.Sehir.FirstOrDefault(s => s.Sehir_Ad == selectedCity)?.Sehir_ID;
                existingAdres.ilce = _context.ilce.FirstOrDefault(i => i.ilce_Ad == selectedIlce);
                existingAdres.Mahalle_ID = _context.Mahalle.FirstOrDefault(m => m.Mahalle_Ad == selectedMahalle)?.Mahalle_ID;
                existingAdres.Sokak_ID = _context.Sokak.FirstOrDefault(s => s.Sokak_Ad == selectedSokak)?.Sokak_ID;
                existingAdres.Bina_No = int.Parse(BinaNo);

                _context.SaveChanges();

                // Başarı durumunu göster
                ViewBag.SuccessMessage = "Settings updated successfully!";
            }
            catch (Exception ex)
            {
                // Hata durumunu göster
                ViewBag.ErrorMessage = "An error occurred while updating settings.";
                Console.WriteLine(ex.Message);
            }



            // Diğer işlemleri yapmak veya sayfaya yönlendirmek isterseniz burada ekleyebilirsiniz
            return Redirect("SettingsPage");
        }
        public JsonResult GetIlceler(int cityId)
        {
            var ilceler = _context.ilce.Where(i => i.Sehir_ID == cityId).ToList();
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ChangePassword()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {
            try
            {
                // Session'dan kullanıcı ID'sini al
                int userId = (int)Session["Kullanici_ID"];

                // Kullanıcıyı veritabanından çek
                var user = _context.Kullanici.Find(userId);

                // Eğer kullanıcı varsa ve eski şifre doğru ise devam et
                if (user != null && user.Sifre == oldPassword)
                {
                    // Yeni şifre ile şifre doğrulamasını kontrol et
                    if (newPassword == confirmNewPassword)
                    {
                        // Yeni şifreyi güncelle
                        user.Sifre = newPassword;
                        _context.SaveChanges();

                        // Başarı mesajını ayarla
                        ViewBag.SuccessMessage = "Password changed successfully!";
                    }
                    else
                    {
                        // Şifre doğrulaması hatalıysa hata mesajını ayarla
                        ViewBag.ErrorMessage = "New password and confirm password do not match.";
                    }
                }
                else
                {
                    // Eğer kullanıcı yoksa veya eski şifre hatalı ise hata mesajını ayarla
                    ViewBag.ErrorMessage = "Invalid old password.";
                }
            }
            catch (Exception ex)
            {
                // Diğer olası hataları kontrol etmek için hata mesajını ayarla
                ViewBag.ErrorMessage = "An error occurred while changing the password.";
                Console.WriteLine(ex.Message);
            }

            // ChangePassword view'ine yönlendir
            return RedirectToAction("ChangePassword");
        }


        public ActionResult HomeAnon()
        {
            return View(_context.Oyuncak.ToList());
        }


        public ActionResult DetailsAnon(int id)
        {
            // ID'yi kullanarak gerekli işlemleri gerçekleştirin
            // Örneğin, ID'yi kullanarak modeli veritabanından çekmek gibi...
            var product = _context.Oyuncak.Find(id);



            // Modeli sayfaya gönderin
            return View("DetailsAnon", product);

        }


        public ActionResult AdminPanel(int id)
        {
            return View();

        }


        public ActionResult AddToCart(int id)
        {


            var product = _context.Oyuncak.Find(id);



            // Modeli sayfaya gönderin
            return View("AddToCart", product);


        }

        public ActionResult Hakkimizda()
        {
            return View();
        }


        private bool ValidatePayment(string ccName, string ccNumber, string ccDate, string ccCvv)
        {

           
            // Basit bir doğrulama: İsim ve numara girilmişse ödeme başarılı kabul edilsin
            return !(string.IsNullOrWhiteSpace(ccName)) && !(string.IsNullOrWhiteSpace(ccNumber)) && !(string.IsNullOrWhiteSpace(ccDate)) && !(string.IsNullOrWhiteSpace(ccCvv));
        }



        [HttpPost]
        
        public ActionResult AddToCart2(string ccName, string ccNumber, string ccCVV, string ccDATE)
        {
            // Ödeme başarılı kriterleri: isim ve numara doğru girilmişse
            bool paymentSuccess = ValidatePayment(ccName, ccNumber, ccCVV, ccDATE);

            if (paymentSuccess)
            {
                TempData["SuccessMessage"] = "Ödeme başarıyla gerçekleşti.";
                return RedirectToAction("Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Ödeme işlemi başarısız oldu. Lütfen geçerli isim ve numara girin.";
                return RedirectToAction("AddToCart");
            }

            
        }

        // Ödeme doğrulama işlemi
        




       

    }
}












//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Linq;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using B201200001_WEB;
//using B201200001_WEB.Models;

//namespace B201200001_WEB.Controllers
//{
//    public class HomeController : Controller
//    {

//        web_programlamaEntities _context = new web_programlamaEntities();
//        // GET: Home
//        public ActionResult Home()
//        {
//            return View(_context.Oyuncak.ToList());
//        }





//        public ActionResult Details(int id)
//        {
//            // ID'yi kullanarak gerekli işlemleri gerçekleştirin
//            // Örneğin, ID'yi kullanarak modeli veritabanından çekmek gibi...
//            var product = _context.Oyuncak.Find(id);



//            // Modeli sayfaya gönderin
//            return View("Details", product);

//        }

//        public ActionResult About()
//        {


//            return View();
//        }

//        public ActionResult AddToCart(int ID)
//        {

//            var product = _context.Oyuncak.Where(i => i.Oyuncak_ID == ID);

//            if (product != null)
//            {
//                //getCart();
//            }

//            return View();
//        }

//        public ActionResult Login()
//        {


//            return View();
//        }


//        [HttpPost]
//        public ActionResult Login(string email, string password)
//        {

//            var user = _context.Kullanici.FirstOrDefault(u => u.Email == email);
//            if (user != null && user.Sifre == password) {

//                Session["Kullanici_ID"] = user.Kullanici_ID;

//                return RedirectToAction("Home", "Home");
//            }

//            else
//            {
//                ViewBag.ErrorMessage = "Invalid email or password";
//                ViewBag.CurrentUser = null;
//                return View();
//            }

//        }


//        public ActionResult SignUp()
//        {

//            return View();
//        }



//        [HttpPost]
//        public ActionResult SignUp(string name, string email, string password, string confirmPassword)
//        {
//            // Şifre doğrulaması yap
//            if (password != confirmPassword)
//            {
//                ViewBag.ErrorMessage = "Password and Confirm Password do not match";
//                return View("SignUp");
//            }

//            // E-posta adresiyle daha önce kayıtlı bir kullanıcı var mı kontrol et
//            var existingUser = _context.Kullanici.FirstOrDefault(u => u.Email == email);
//            if (existingUser != null)
//            {
//                ViewBag.ErrorMessage = "Email address is already registered";
//                return View("SignUp");
//            }

//            // Yeni kullanıcı oluştur ve veritabanına ekle
//            var newUser = new Kullanici { Kullanici_Ad = name, Email = email, Sifre = password, Admin_Situation=false};





//            _context.Kullanici.Add(newUser);
//            _context.SaveChanges();

//            // Kullanıcıyı giriş sayfasına yönlendir
//            return RedirectToAction("SettingsPage", new { userId = newUser.Kullanici_ID });
//        }





//        public ActionResult SettingsPage()
//        {
//            int userId = (int)Session["Kullanici_ID"];
//            var user = _context.Kullanici.Find(userId);

//            // Kullanıcının adres bilgilerini al
//            var adres = _context.Adres.FirstOrDefault(a => a.Adres_ID == user.Adres_ID);

//            if (adres == null)
//            {
//                // Kullanıcının adres bilgisi yoksa CreateAdress sayfasına yönlendir
//                return RedirectToAction("CreateAdress", new { userId = userId });
//            }

//            AdresViewModel adresViewModel = new AdresViewModel
//            {
//                Sehir = _context.Sehir.FirstOrDefault(s => s.Sehir_ID == adres.Sehir_ID)?.Sehir_Ad,
//                Ilce = _context.ilce.FirstOrDefault(i => i.ilce_ID == adres.ilce_ID)?.ilce_Ad,
//                Mahalle = _context.Mahalle.FirstOrDefault(m => m.Mahalle_ID == adres.Mahalle_ID)?.Mahalle_Ad,
//                Sokak = _context.Sokak.FirstOrDefault(s => s.Sokak_ID == adres.Sokak_ID)?.Sokak_Ad,
//                BinaNo = adres.Bina_No
//            };

//            var cities = _context.Sehir.ToList();
//            var ilceler = _context.ilce.ToList();
//            var mahalleler = _context.Mahalle.ToList();
//            var sokaklar = _context.Sokak.ToList();

//            var SettingsModel = new SettingsViewModel
//            {
//                User = user,
//                Adres = adresViewModel,
//                Cities = cities,
//                Ilceler = ilceler,
//                Mahalleler = mahalleler,
//                Sokaklar = sokaklar
//            };

//            // Modeli sayfaya gönderin
//            return View("SettingsPage", SettingsModel);
//        }



//        public ActionResult CreateAdress()
//        {
//            var cities = _context.Sehir.ToList();
//            var ilceler = _context.ilce.ToList();
//            var mahalleler = _context.Mahalle.ToList();
//            var sokaklar = _context.Sokak.ToList();

//            var createAdressModel = new CreateAdressViewModel
//            {
//                Cities = cities,
//                Ilceler = ilceler,
//                Mahalleler = mahalleler,
//                Sokaklar = sokaklar
//            };




//            return View(createAdressModel);
//        }




//        [HttpPost]
//        public ActionResult CreateAdress(string selectedCity, string selectedIlce, string selectedMahalle, string selectedSokak, string BinaNo)
//        {
//            try
//            {
//                int userId = (int)Session["Kullanici_ID"];
//                var user = _context.Kullanici.Find(userId);

//                // Kullanıcının adresi zaten var mı kontrol et
//                if (user.Adres_ID != null)
//                {
//                    // Kullanıcının zaten bir adresi varsa SettingsPage'e yönlendir
//                    return RedirectToAction("SettingsPage");
//                }

//                // Yeni bir adres oluştur
//                var newAdres = new Adres
//                {
//                    Sehir_ID = _context.Sehir.FirstOrDefault(s => s.Sehir_Ad == selectedCity)?.Sehir_ID,
//                    ilce_ID = _context.ilce.FirstOrDefault(i => i.ilce_Ad == selectedIlce)?.ilce_ID,
//                    Mahalle_ID = _context.Mahalle.FirstOrDefault(m => m.Mahalle_Ad == selectedMahalle)?.Mahalle_ID,
//                    Sokak_ID = _context.Sokak.FirstOrDefault(s => s.Sokak_Ad == selectedSokak)?.Sokak_ID,
//                    Bina_No = int.Parse(BinaNo),
//                    Kullanici_ID = userId  // Yeni adresi oluştururken kullanıcının ID'sini belirt


//            };

//                // Yeni adresi kullanıcıya bağla
//                user.Adres_ID = newAdres.Adres_ID;

//                // Veritabanına değişiklikleri kaydet
//                _context.Entry(user).State = EntityState.Modified;
//                _context.SaveChanges();

//                // Başarı durumunu göster
//                ViewBag.SuccessMessage = "Address created successfully!";
//            }
//            catch (Exception ex)
//            {
//                // Hata durumunu göster
//                ViewBag.ErrorMessage = "An error occurred while creating the address.";
//                Console.WriteLine(ex.Message);
//            }


//            // Diğer işlemleri yapmak veya sayfaya yönlendirmek isterseniz burada ekleyebilirsiniz
//            return RedirectToAction("SettingsPage");
//        }



//        [HttpPost]
//        public ActionResult SettingsPage(string selectedCity, string selectedIlce, string selectedMahalle, string selectedSokak, string BinaNo)
//        {
//            try
//            {
//                int userId = (int)Session["Kullanici_ID"];
//                var user = _context.Kullanici.Find(userId);
//                var existingAdres = _context.Adres.FirstOrDefault(a => a.Adres_ID == user.Adres_ID);

//                if (existingAdres == null)
//                {
//                    existingAdres = new Adres();
//                    _context.Adres.Add(existingAdres);
//                }

//                // Yeni adres bilgilerini güncelle
//                existingAdres.Sehir_ID = _context.Sehir.FirstOrDefault(s => s.Sehir_Ad == selectedCity)?.Sehir_ID;
//                existingAdres.ilce = _context.ilce.FirstOrDefault(i => i.ilce_Ad == selectedIlce);
//                existingAdres.Mahalle_ID = _context.Mahalle.FirstOrDefault(m => m.Mahalle_Ad == selectedMahalle)?.Mahalle_ID;
//                existingAdres.Sokak_ID = _context.Sokak.FirstOrDefault(s => s.Sokak_Ad == selectedSokak)?.Sokak_ID;
//                existingAdres.Bina_No = int.Parse(BinaNo);

//                _context.SaveChanges();

//                // Başarı durumunu göster
//                ViewBag.SuccessMessage = "Settings updated successfully!";
//            }
//            catch (Exception ex)
//            {
//                // Hata durumunu göster
//                ViewBag.ErrorMessage = "An error occurred while updating settings.";
//                Console.WriteLine(ex.Message);
//            }



//            // Diğer işlemleri yapmak veya sayfaya yönlendirmek isterseniz burada ekleyebilirsiniz
//            return Redirect("SettingsPage");
//        }







//        //public JsonResult GetIlceler(int cityId)
//        //{
//        //    var ilceler = _context.ilce.Where(i => i.Sehir_ID == cityId).ToList();
//        //    return Json(ilceler, JsonRequestBehavior.AllowGet);
//        //}


//        public ActionResult ChangePassword()
//        {

//            return View();
//        }


//        [HttpPost]
//        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
//        {
//            try
//            {
//                // Session'dan kullanıcı ID'sini al
//                int userId = (int)Session["Kullanici_ID"];

//                // Kullanıcıyı veritabanından çek
//                var user = _context.Kullanici.Find(userId);

//                // Eğer kullanıcı varsa ve eski şifre doğru ise devam et
//                if (user != null && user.Sifre == oldPassword)
//                {
//                    // Yeni şifre ile şifre doğrulamasını kontrol et
//                    if (newPassword == confirmNewPassword)
//                    {
//                        // Yeni şifreyi güncelle
//                        user.Sifre = newPassword;
//                        _context.SaveChanges();

//                        // Başarı mesajını ayarla
//                        ViewBag.SuccessMessage = "Password changed successfully!";
//                    }
//                    else
//                    {
//                        // Şifre doğrulaması hatalıysa hata mesajını ayarla
//                        ViewBag.ErrorMessage = "New password and confirm password do not match.";
//                    }
//                }
//                else
//                {
//                    // Eğer kullanıcı yoksa veya eski şifre hatalı ise hata mesajını ayarla
//                    ViewBag.ErrorMessage = "Invalid old password.";
//                }
//            }
//            catch (Exception ex)
//            {
//                // Diğer olası hataları kontrol etmek için hata mesajını ayarla
//                ViewBag.ErrorMessage = "An error occurred while changing the password.";
//                Console.WriteLine(ex.Message);
//            }

//            // ChangePassword view'ine yönlendir
//            return RedirectToAction("ChangePassword");
//        }



//        public ActionResult HomeAnon()
//        {
//            return View(_context.Oyuncak.ToList());
//        }


//        public ActionResult DetailsAnon(int id)
//        {
//            // ID'yi kullanarak gerekli işlemleri gerçekleştirin
//            // Örneğin, ID'yi kullanarak modeli veritabanından çekmek gibi...
//            var product = _context.Oyuncak.Find(id);



//            // Modeli sayfaya gönderin
//            return View("DetailsAnon", product);

//        }




//    }
//}



