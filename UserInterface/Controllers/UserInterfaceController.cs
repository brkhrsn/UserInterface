using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    //Burada kuyruk yapısının Enqueue methodunu override ederek değiştirdik ve kuyruktan çıkan ilk nesneyi mail olarak attık.
    public override void Enqueue(object obj)
    {
    base.Enqueue(obj);
    OnChanged(SendMail());
    }
    public class UserInterfaceController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        //Sayfayı sadece görüntülemek istediğimde yani post olmadığında, bu kısım çalışacak.
        public ActionResult Create()
        {
            return View();
        }

        // Buraya HttpPost eklememizin nedeni, bu action sadece post olunca çalışacak olması.
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            //Burada kuyruk nesnesini oluşturduk ve kuyruğa davetli kullanıcının email adresini ekledik.
            Queue sira = new Queue();
            sira.Enqueue(form["DavetEmail"].Trim());

            //Models klasörüne ADO.NET Entity Data Model ekledik ve adına FormDB dedik. 
            //Normalde ADO.NET Entity Data Model eklerken "Generate From Database" seçeneğini seçeriz ve önceden tasarladığımız veritabanını seçeriz
            //Ancak burada ben önceden database oluşturmadığım için Empty seçeneğini seçtim.
         
            FormDBEntities db = new FormDBEntities();
            Users model = new User();
            model.UserName = form["Name"].Trim();
            model.Surname = form["SurName"].Trim();
            model.Email = form["Email"].Trim();
            db.Users.add(model);
            db.SaveChanges();

            return View();
        }
    }
}
