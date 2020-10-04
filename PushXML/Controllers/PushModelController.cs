using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using PushXML.Models;

namespace PushXML.Controllers
{
    public class PushModelController : Controller
    {
       
       
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PushModel
        public ActionResult Index()
        {
            return View(db.PushModel.ToList());
        }

        // GET: PushModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PushModel pushModel = db.PushModel.Find(id);
            if (pushModel == null)
            {
                return HttpNotFound();
            }
            return View(pushModel);
        }

        // GET: PushModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PushModel/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PushId,IdAsiento,NoAsiento,DescripcionAsiento,FechaAsiento,CuentaContable,TipoMovimiento,Monto")] PushModel pushModel)
        {
            if (ModelState.IsValid)
            {
                db.PushModel.Add(pushModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pushModel);
        }

        // GET: PushModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PushModel pushModel = db.PushModel.Find(id);
            if (pushModel == null)
            {
                return HttpNotFound();
            }
            return View(pushModel);
        }

        // POST: PushModel/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PushId,IdAsiento,NoAsiento,DescripcionAsiento,FechaAsiento,CuentaContable,TipoMovimiento,Monto")] PushModel pushModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pushModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pushModel);
        }

        // GET: PushModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PushModel pushModel = db.PushModel.Find(id);
            if (pushModel == null)
            {
                return HttpNotFound();
            }
            return View(pushModel);
        }

        // POST: PushModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PushModel pushModel = db.PushModel.Find(id);
            db.PushModel.Remove(pushModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        public ActionResult GenerarXML()
        {
            var push1 = db.PushModel.ToList();
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;
            //Converting List to XML using LINQ to XML    
            // the xml doc will get stored into OrderDetails object of XDocument    
            using (XmlWriter xw = XmlWriter.Create(ms, xws))
            {
                XDocument doc = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                from Push in push1
                select new XElement("AsientoContable",
                new XElement("IdAsiento", Push.IdAsiento),
                new XElement("NoAsiento", Push.NoAsiento),
                new XElement("DescripcionAsiento", Push.DescripcionAsiento),
                new XElement("FechaAsiento", Push.FechaAsiento),
                new XElement("CuentaContable", Push.CuentaContable),
                new XElement("TipoMovimiento", Push.TipoMovimiento),
                new XElement("Monto", Push.Monto))
                );
                doc.WriteTo(xw);
            }
            ms.Position = 0;
            return File(ms, "text/xml", "PushAsiento.xml");

        
        }

    }
}
