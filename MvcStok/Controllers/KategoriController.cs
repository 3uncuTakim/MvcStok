using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;//Şayet bu modeli tanıtmazsam çalışmaz.

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities1 db=new MvcDbStokEntities1(); //Tablolara ulaşmak için db nesnesi türetiyorum.
        //Bu benim modelimi tutuyor. Modelimde de tablolarım var.
        public ActionResult Index()
        {
            var degerler=db.TBLKATEGORILER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View(); //Sayfa çağrıldıgında sadece view getirsin. yoksa her sayfa açıldıgında kategori ekler.
            //Thats why we use httpget and httppost. these are different things..
        }


        [HttpPost] //Butona tıkladıgımda çalışacak.
        public ActionResult YeniKategori(TBLKATEGORILER p1) //Kategori ekleme için böyle bir şey yaptım. parametre1
        {
            db.TBLKATEGORILER.Add(p1); //Gelen parametreyi tbl kategorilere ekle
            db.SaveChanges();
            return View();
        }

        public ActionResult SIL(int id) //Silme işlemi için id değişkeni tanımladım. ID'ye göre sildireceğim.
        {
            var kategori=db.TBLKATEGORILER.Find(id); //TBLKAtegorilerde bul neyi bul id'den gelen değeri bul.
            db.TBLKATEGORILER.Remove(kategori);//TBLKAtegorilerdeki değeri sil neyi sil kategori değişkeninden geleni.
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GUNCELLE(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir",ktgr);
        }
    }
}