using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace hpl.Controllers
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

        public ActionResult Login()
        {
            var f = Request.Form;
            return View();
        }

        [HttpPost]
        public ActionResult Login(int id, string name, string password, string reg)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hplo = new MySqlConnection(hp);
            hplo.Open();

            string str = @"insert into userdata (id,name,password) values(" + id  + "," + "'name'" + "," + "'password'" + ")";
            MySqlCommand logcom = new MySqlCommand(str, hplo);
            MySqlDataReader sql = logcom.ExecuteReader();

            hplo.Close();

            return View();



            //int hpinline = inscom.ExecuteNonQuery();
            //if(hpinline > 0)
            //{

            //}
            //else
            //{
            //  return View("Registration");
            //}
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(string id, string name, string password, string email, string remark)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hpin = new MySqlConnection(hp);
            hpin.Open();
            MySqlCommand inscom = new MySqlCommand("INSERT INTO userdate VALUES(" + "' name'" + "," + "' password'" + "'email'" + "'remark'" + ")", hpin);
            MySqlDataReader sql = inscom.ExecuteReader();

            hpin.Close();

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Message = "Your List page.";

            return View();
        }
    }
}