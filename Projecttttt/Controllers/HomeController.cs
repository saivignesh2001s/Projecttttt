using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using BAL;
using DAL;

namespace Projecttttt.Controllers
{
    public class HomeController : Controller
    {
        flightmethods ds = null;
        public HomeController() {
             ds = new flightmethods();
        }
        public ActionResult Insertfile() { 
            return View();
        }
        [HttpPost]
        public ActionResult Insertfile(FormCollection c)
        {
            string k = c["Name"].ToString();
            string p = "D:\\" + k;
            FileStream fs = new FileStream(p, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            bool h = false;
            while (!sr.EndOfStream)
            {
                p = sr.ReadLine();
                string[] k1 = p.Split(',');
                flight p1 = new flight();
                if (k1[0] != null)
                {
                    p1.flightid = k1[0].ToString();
                }
                if (k1[1] != null)
                {
                    p1.flightname= k1[1].ToString();
                }
                bool poo = ds.addmethod(p1);
                if (!poo) {
                    h = true;
                 ViewBag.Text1="Irrelevant data";
                    break;
                }
               

            }
            if (h) {
                ViewBag.Text2 = "Data not uploaded";
                return View();

            }
            else
            {
                sr.Close();
                fs.Close();
                fs.Dispose();

                return RedirectToAction("Index");
            }


        }
        public ActionResult Index() {
            List<flight> jj = ds.flightlist();
            return View(jj);
        }
        public ActionResult Index2() {
            string po = "D:\\file1.csv";
            FileStream fs = new FileStream(po, FileMode.CreateNew,FileAccess.Write);
            StreamWriter ws=new StreamWriter(fs);
            List<flight> jj = ds.flightlist();
            bool p3 = false;
            try
            {
                foreach (var i in jj)
                {

                    ws.WriteLine(i.flightid + ',' + i.flightname);

                }
            }
            catch{
                p3 = true;
            
            }
            if (p3)
            {
                ViewBag.Text22 = "Exported successfully";
                return View("Index");
            }
            else {

                ViewBag.Text23 = "  Error...Not Exported successfully";
                return View("Index");
            }
        }
    }
}