using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;//İlk olarak using yapıyoruz. Projedeki Modeldeki Entitiyi kullan

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        //Ardından db üretiyoruz ki moıdele erişebillim.
        MvcDbStokEntities1 db = new MvcDbStokEntities1();
        public ActionResult Index() //Son olarak da index'e view ekliyoruz.
        {
            //Ardından ürünleri listede tutacak değişkeni tanımlamak lazım.
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler); //Tabi bu değişkeni viewda return ettirmemiz lazım..
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            //Aşağıdaki liste kategorileri sıralı getirmek için. linq sorgusu var. Dropdown'u araştır.
            //SelectListItem Sadewce orada kullanılıyor.
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text=i.KATEGORIAD,
                                                 Value=i.KATEGORIID.ToString(),
                                             }).ToList();
            ViewBag.dgr=degerler; //Controller'dan diğer tarafa almak için view'ü çantaya koymak lazım.
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault(); //Dropdown list için.
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}