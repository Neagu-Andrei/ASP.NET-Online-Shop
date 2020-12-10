using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppContext = ProiectBun.Models.AppContext;
using System.Web.Mvc;
using System.IO;
using ProiectBun.Models;
using File = ProiectBun.Models.File;

namespace ProiectBun.Controllers
{
    public class FilesController : Controller
    {
        private AppContext db = new AppContext();
        // GET: Files
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadedFile)
        {
            string uploadedFileName = uploadedFile.FileName;
            string uploadedFileExtension = Path.GetExtension(uploadedFileName);

            if(uploadedFileExtension == ".png" || uploadedFileExtension == ".jpg" || uploadedFileExtension == ".gif")
            {
                string uploadFolderPath = Server.MapPath("~//Files//");

                uploadedFile.SaveAs(uploadFolderPath + uploadedFileName);

                File file = new File();
                file.Extension = uploadedFileExtension;
                file.FileName = uploadedFileName;
                file.FilePath = uploadFolderPath + uploadedFileName;

                db.Files.Add(file);
                return Redirect("Index");
            }
            return View();
        }
    }
}