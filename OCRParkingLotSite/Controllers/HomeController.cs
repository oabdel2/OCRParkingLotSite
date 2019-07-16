using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using openalprnet;

namespace OCRParkingLotSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                // file is uploaded
                file.SaveAs(Server.MapPath("~/App_Data/images/") + (Guid.NewGuid().ToString()) + ".png");

                byte[] arr = new byte[1];
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    arr = ms.GetBuffer();
                }


                var alpr = new AlprNet("us", "C:\\Users\\oabdel2\\Downloads\\openalpr-2.3.0-win-64bit\\openalpr_64\\openalpr.conf", "C:\\Users\\oabdel2\\Downloads\\openalpr-2.3.0-win-64bit\\openalpr_64\\runtime_data");
                if (!alpr.IsLoaded())
                {
                    Console.WriteLine("OpenAlpr failed to load!");
                    
                }
                // Optionally apply pattern matching for a particular region
                alpr.DefaultRegion = "md";

                
                var results = alpr.Recognize(arr);

                string test = results.Plates.First().TopNPlates.First().ToString();
                int a = 1;
                
            }
            // after successfully uploading redirect the user
            return View("Index");
        }

    }
}