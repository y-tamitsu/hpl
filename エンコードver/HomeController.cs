using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using hpl.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Net;

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

        //ログイン
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(userModel model, int id, string password)
        {
            try
            {
                string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(hp))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT* FROM userdata where id = " + id + " and password = '" + password + "' ORDER BY id ASC;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                // ユーザー認証 成功
                                return Redirect("List");
                            }
                            else
                            {   //ユーザー認証 失敗
                                ModelState.AddModelError(string.Empty, "指定されたユーザー名またはパスワードが正しくありません。");
                                return View();
                            }
                        }
                    }
                }
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.Message;
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        public ActionResult Logout()
        {
            ViewBag.Message = "Your Logout page.";
            return View();
        }

        [HttpPost]
        public ActionResult Logout(int kari)
        {
            ViewBag.Message = "Your Logout page.";
            return View();
        }

        //登録
        //[HttpGet]
        //public ActionResult Registration()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Registration(userModel model)
        //{
        //    try
        //    {
        //        string ihp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;

        //        using (MySqlConnection hpin = new MySqlConnection(ihp))
        //        {
        //            hpin.Open();
        //            using (MySqlCommand cmd = hpin.CreateCommand())
        //            {
        //                cmd.CommandText = @"select * from userdata where ( id = " + model.id + ")";
        //                using (MySqlDataReader sel = cmd.ExecuteReader())
        //                {
        //                    if (sel.Read() == true)
        //                    {
        //                        ModelState.AddModelError(string.Empty, "入力したユーザーIDは既に使用されています。");
        //                        return View();
        //                    }
        //                    else
        //                    {
        //                        sel.Close();
        //                        cmd.CommandText = @"insert ignore into userdata (id,name,password,email,remark) values(" + model.id + ",'" + model.name + "','" + model.password + "','" + model.mail + "','" + model.remark + "')";
        //                        cmd.ExecuteNonQuery();
        //                        return RedirectToAction("List");

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception er)
        //    {
        //        byte[] error;
        //        using (System.IO.MemoryStream ms = new MemoryStream())
        //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
        //        {
        //            string raw = er.Message;
        //            sw.WriteLine(raw);
        //            sw.Flush();
        //            error = ms.ToArray();
        //        }
        //        return File(error, "text", "error.txt");
        //    }
        //}

        //一覧
        //public ActionResult List()
        //{
        //    Session["and"] = "checked";
        //    string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
        //    var userList = new List<userModel>();
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(hp))
        //        {
        //            conn.Open();
        //            using (MySqlCommand cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = $"select * from userdata ORDER BY id ASC;";
        //                using (MySqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        userList.Add(new userModel { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), password = reader["password"].ToString(), mail = reader["email"].ToString(), remark = reader["remark"].ToString() });
        //                    }
        //                }
        //            }
        //        }
        //        return View(userList);
        //    }
        //    catch (System.Exception er)
        //    {
        //        byte[] error;
        //        using (System.IO.MemoryStream ms = new MemoryStream())
        //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
        //        {
        //            string raw = er.Message;
        //            sw.WriteLine(raw);
        //            sw.Flush();
        //            error = ms.ToArray();
        //        }
        //        return File(error, "text", "error.txt");
        //    }
        //}

        ////絞り込み処理
        //[HttpPost]
        //public ActionResult List(string select, string example1, string example2, string serch1, string serch2)
        //{
        //    string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
        //    MySqlConnection hpsel = new MySqlConnection(hp);
        //    hpsel.Open();
        //    var userList = new List<userModel>();
        //    int i1 = 0, i2 = 0;
        //    bool r1 = int.TryParse(serch1, out i1);
        //    bool r2 = int.TryParse(serch2, out i2);
        //    Session["select"] = select;
        //    Session["example1"] = example1;
        //    Session["example2"] = example2;
        //    Session["serch1"] = serch1;
        //    Session["serch2"] = serch2;

        //    if (select == "and")
        //    {
        //        Session["and"] = null;
        //        Session["or"] = null;
        //        Session["and"] = "checked";
        //    }
        //    else if (select == "or")
        //    {
        //        Session["and"] = null;
        //        Session["or"] = null;
        //        Session["or"] = "checked";
        //    }

        //    if (example1 == "id")
        //    {
        //        Session["selid"] = null;
        //        Session["selna"] = null;
        //        Session["selem"] = null;
        //        Session["selid"] = "selected";
        //    }
        //    else if (example1 == "name")
        //    {
        //        Session["selid"] = null;
        //        Session["selna"] = null;
        //        Session["selem"] = null;
        //        Session["selna"] = "selected";
        //    }
        //    else if (example1 == "email")
        //    {
        //        Session["selid"] = null;
        //        Session["selna"] = null;
        //        Session["selem"] = null;
        //        Session["selem"] = "selected";
        //    }

        //    if (example2 == "id")
        //    {
        //        Session["selid2"] = null;
        //        Session["selna2"] = null;
        //        Session["selem2"] = null;
        //        Session["selid2"] = "selected";
        //    }
        //    else if (example2 == "name")
        //    {
        //        Session["selid2"] = null;
        //        Session["selna2"] = null;
        //        Session["selem2"] = null;
        //        Session["selna2"] = "selected";
        //    }
        //    else if (example2 == "email")
        //    {
        //        Session["selid2"] = null;
        //        Session["selna2"] = null;
        //        Session["selem2"] = null;
        //        Session["selem2"] = "selected";
        //    }

        //    try
        //    {
        //        string comand = "";
        //        if (string.IsNullOrEmpty(serch2))
        //        {
        //            if ((example1 == "id" && r1) || (example1 == "name" && r1) || (example1 == "email" && r1))
        //            {
        //                if ((example1 == "id") && r1)
        //                {
        //                    comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'";
        //                }
        //                else if ((example1 == "name") || (example1 == "email"))
        //                {
        //                    comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'";
        //                }
        //                else
        //                {
        //                    ViewBag.error = "IDを数値以外で検索することはできません";
        //                }
        //                using (MySqlConnection conn = new MySqlConnection(hp))
        //                {
        //                    conn.Open();
        //                    using (MySqlCommand cmd = conn.CreateCommand())
        //                    {
        //                        cmd.CommandText = $"select * from userdata {comand} ORDER BY id ASC;";
        //                        using (MySqlDataReader reader = cmd.ExecuteReader())
        //                        {
        //                            while (reader.Read())
        //                            {
        //                                userList.Add(new userModel { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), password = reader["password"].ToString(), mail = reader["email"].ToString(), remark = reader["remark"].ToString() });
        //                            }
        //                        }
        //                    }
        //                }
        //                return View(userList);
        //            }
        //        }
        //        else
        //        {
        //            if ((example1 == "id" && example2 == "id") || (example1 == "id" && example2 == "name") || (example1 == "id" && example2 == "email") || (example1 == "name" && example2 == "id") || (example1 == "email" && example2 == "id"))
        //            {
        //                if ((example1 == "id" && r1) || (example2 == "id" && r2))
        //                    comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%'";
        //                else
        //                    ViewBag.error = "IDを数値以外で検索することはできません";
        //            }
        //            if ((example1 == "name" && example2 == "name") || (example1 == "name" && example2 == "email") || (example1 == "email" && example2 == "name") || (example1 == "email" && example2 == "email"))
        //                comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%'";

        //            using (MySqlConnection conn = new MySqlConnection(hp))
        //            {
        //                conn.Open();
        //                using (MySqlCommand cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandText = $"select * from userdata {comand} ORDER BY id ASC;";
        //                    using (MySqlDataReader reader = cmd.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            userList.Add(new userModel { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), password = reader["password"].ToString(), mail = reader["email"].ToString(), remark = reader["remark"].ToString() });
        //                        }
        //                    }
        //                }
        //            }
        //            return View(userList);


        //        }

        //        using (MySqlConnection conn = new MySqlConnection(hp))
        //        {
        //            conn.Open();
        //            using (MySqlCommand cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = $"select * from userdata ORDER BY id ASC;";
        //                using (MySqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        userList.Add(new userModel { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), password = reader["password"].ToString(), mail = reader["email"].ToString(), remark = reader["remark"].ToString() });
        //                    }
        //                }
        //            }
        //        }
        //        return View(userList);
        //    }
        //    catch (System.Exception er)
        //    {
        //        byte[] error;
        //        using (System.IO.MemoryStream ms = new MemoryStream())
        //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
        //        {
        //            string raw = er.Message;
        //            sw.WriteLine(raw);
        //            sw.Flush();
        //            error = ms.ToArray();
        //        }
        //        return File(error, "text", "error.txt");
        //    }
        //}

        //public ActionResult Delete(int id, string name, string password, string mail, string remark)
        //{
        //    try
        //    {
        //        ViewBag.id = id;
        //        ViewBag.name = name;
        //        ViewBag.pass = password;
        //        ViewBag.mail = mail;
        //        ViewBag.remark = remark;

        //        return View();
        //    }
        //    catch (System.Exception er)
        //    {
        //        byte[] error;
        //        using (System.IO.MemoryStream ms = new MemoryStream())
        //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
        //        {
        //            string raw = er.Message;
        //            sw.WriteLine(raw);
        //            sw.Flush();
        //            error = ms.ToArray();
        //        }
        //        return File(error, "text", "error.txt");
        //    }
        //}

        ////削除
        //[HttpPost]
        //public ActionResult Delete(int id, string name)
        //{
        //    string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
        //    MySqlConnection hpdel = new MySqlConnection(hp);
        //    hpdel.Open();
        //    try
        //    {
        //        string delstr = @"delete from userdata where id = @id";
        //        MySqlCommand delid = new MySqlCommand(delstr, hpdel);
        //        MySqlParameter did = new MySqlParameter("@id", MySqlDbType.Int16, 5);
        //        did.Direction = ParameterDirection.Input;
        //        did.Value = id;
        //        delid.Parameters.Add(did);
        //        delid.ExecuteNonQuery();
        //        return RedirectToAction("List");
        //    }
        //    catch (System.Exception er)
        //    {
        //        byte[] error;
        //        using (System.IO.MemoryStream ms = new MemoryStream())
        //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
        //        {
        //            string raw = er.Message;
        //            sw.WriteLine(raw);

        //            sw.Flush();
        //            error = ms.ToArray();
        //        }
        //        return File(error, "text", "error.txt");
        //    }
        //    finally
        //    {
        //        hpdel.Close();
        //    }
        //}

        //編集
        //public ActionResult Edit(int id, string name, string password, string mail, string remark)
        //{
        //    try
        //    {
        //        ViewBag.id = id;
        //        return View();
        //    }
        //    catch (System.Exception er)
        //    {
        //        byte[] error;
        //        using (System.IO.MemoryStream ms = new MemoryStream())
        //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
        //        {
        //            string raw = er.Message;
        //            sw.WriteLine(raw);
        //            sw.Flush();
        //            error = ms.ToArray();
        //        }
        //        return File(error, "text", "error.txt");
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(userModel model)
        //{
        //    string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
        //    MySqlConnection hped = new MySqlConnection(hp);
        //    hped.Open();
        //    try
        //    {
        //        string edstr = @"update ignore userdata set id = " + model.id + ", name = '" + model.name + "', password = '" + model.password + "', email = '" + model.mail + "', remark = '" + model.remark + "'where id = @id;";
        //        MySqlCommand edit = new MySqlCommand(edstr, hped);
        //        MySqlParameter ed = new MySqlParameter("@id", MySqlDbType.Int16);
        //        ed.Direction = ParameterDirection.Input;
        //        ed.Value = model.id;
        //        edit.Parameters.Add(ed);
        //        edit.ExecuteScalar();
        //        hped.Close();
        //        return RedirectToAction("List");
        //    }
        //    catch (System.Exception er)
        //    {
        //        byte[] error;
        //        using (System.IO.MemoryStream ms = new MemoryStream())
        //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
        //        {
        //            string raw = er.Message;
        //            sw.WriteLine(raw);
        //            sw.Flush();
        //            error = ms.ToArray();
        //        }
        //        return File(error, "text", "error.txt");
        //    }
        //    finally
        //    {
        //        hped.Close();
        //    }
        //}

        //出力処理
        public ActionResult Export(userModel model)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            try
            {
                var userList = new List<string>();
                string sqlWhere = "";

                if (Session["serch1"] != null)
                {
                    sqlWhere = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%'";
                }

                using (MySqlConnection conn = new MySqlConnection(hp))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"select * from userdata {sqlWhere} ORDER BY id ASC;";

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userList.Add(reader["id"].ToString() + "," + reader["name"].ToString() + "," + reader["password"].ToString() + "," + reader["email"].ToString() + "," + reader["remark"].ToString());
                            }
                        }
                    }
                }

                byte[] csv;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    foreach (string row in userList)
                    {
                        sw.WriteLine(row);
                    }
                    sw.Flush();
                    csv = ms.ToArray();
                }
                return File(csv, "text/csv", "userdata.csv");


            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);

                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        [HttpPost]
        public ActionResult Import(userModel model)
        {
            if (Request.Files.Count == 0)
            {
                ViewBag.error = "エラーです";
            }
            var file = Request.Files[0];

            file.SaveAs("C:\\Users\\y-tamitsu\\Desktop");

            Session["path"] = "C:\\Users\\y - tamitsu\\Desktop";
            string con = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hped = new MySqlConnection(con);
            hped.Open();
            try
            {
                string exstr = @"load data local infile " + Session["path"] + " into table userdata fields terminated by ',';";
                MySqlCommand exit = new MySqlCommand(exstr, hped);
                MySqlParameter ex = new MySqlParameter("@id", MySqlDbType.Int64);
                ex.Direction = ParameterDirection.Input;
                ex.Value = model.id;
                exit.Parameters.Add(ex);
                exit.ExecuteNonQuery();
                hped.Close();
                return RedirectToAction("List");
            }

            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);

                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
            finally
            {
                hped.Close();
            }
        }

        public ActionResult Error()
        {
            ViewBag.ce = Session["error"];
            return View();
        }


        //　ここからオークション

        //出品情報　登録
        public ActionResult ExhibitReg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExhibitReg(userModel model, HttpPostedFileWrapper file)
        {
            try
            {
                var files = Request.Files;
                DateTime nowtime = DateTime.Now;
                DateTime weektime = nowtime.AddDays(7);
                string path = System.IO.Path.GetFileName(file.FileName);
                if (file.FileName == "")
                {
                    ViewBag.imger = "エラーです";
                }
                else
                {

                    file.SaveAs("C:\\UploadedFiles\\" + path);
                }



                string ihp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
                using (MySqlConnection hpin = new MySqlConnection(ihp))
                {
                    using (MySqlCommand cmd = hpin.CreateCommand())
                    {
                        hpin.Open();
                        cmd.CommandText = @"select * from exhibit where ( id = " + model.id + ")";
                        using (MySqlDataReader sel = cmd.ExecuteReader())
                        {
                            if (sel.Read() == true)
                            {
                                ModelState.AddModelError(string.Empty, "入力したユーザーIDは既に使用されています。");
                                return View();
                            }
                            else
                            {
                                sel.Close();
                                cmd.CommandText = @"insert ignore into exhibit (id,title,detail,money,price,time,lasttime,image) values(" + model.id + ",'" + model.title + "','" + model.detail + "','" + model.money + "','" + model.price + "','" + nowtime + "','" + weektime + "',load_file(" + "'C:/UploadedFiles/" + path + "'))";
                                cmd.ExecuteNonQuery();
                                return RedirectToAction("AcList");
                            }
                        }
                    }
                }
            }

            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        // selectメソッド
        public List<userModel> GetUserList()
        {
            //dbから値を取得
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var AcList = new List<userModel>();

            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from exhibit ORDER BY id ASC;";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AcList.Add(new userModel { id = int.Parse(reader["id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), price = reader["price"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                        }
                    }
                }
            }
            return AcList;
        }

        // id による select imageメソッド
        public userModel GetUser(int id)
        {
            //Where id = idのSelect
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new userModel();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"select image from exhibit where ( id = " + id + ")";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new userModel { image = System.Convert.ToBase64String((byte[])reader["image"]) };
                    return result;
                }
            }
        }

        //出品情報　リスト
        public ActionResult AcList(userModel model)
        {
            Session["and"] = "checked";
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var AcList = new List<userModel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(hp))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"select * from exhibit ORDER BY id ASC;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AcList.Add(new userModel { id = int.Parse(reader["id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), price = reader["price"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                            }
                        }
                    }
                }
                return View(AcList);
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        //リスト絞り込み
        [HttpPost]
        public ActionResult AcList(string select, string ex, string sort, string acde, string example1, string example2, string serch1, string serch2)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hpsel = new MySqlConnection(hp);
            hpsel.Open();
            var AcList = new List<userModel>();
            int i1 = 0, i2 = 0;
            bool r1 = int.TryParse(serch1, out i1);
            bool r2 = int.TryParse(serch2, out i2);
            Session["select"] = select;
            Session["ex"] = ex;
            Session["sort"] = sort;
            Session["acde"] = acde;
            Session["example1"] = example1;
            Session["example2"] = example2;
            Session["serch1"] = serch1;
            Session["serch2"] = serch2;

            if (select == "and")
            {
                Session["and"] = null;
                Session["or"] = null;
                Session["and"] = "checked";
            }
            else if (select == "or")
            {
                Session["and"] = null;
                Session["or"] = null;
                Session["or"] = "checked";
            }

            if (example1 == "id")
            {
                Session["selid"] = null;
                Session["selna"] = null;
                Session["selem"] = null;
                Session["selid"] = "selected";
            }
            else if (example1 == "title")
            {
                Session["selid"] = null;
                Session["selna"] = null;
                Session["selem"] = null;
                Session["selna"] = "selected";
            }
            else if (example1 == "detail")
            {
                Session["selid"] = null;
                Session["selna"] = null;
                Session["selem"] = null;
                Session["selem"] = "selected";
            }

            if (example2 == "id")
            {
                Session["selid2"] = null;
                Session["selna2"] = null;
                Session["selem2"] = null;
                Session["selid2"] = "selected";
            }
            else if (example2 == "title")
            {
                Session["selid2"] = null;
                Session["selna2"] = null;
                Session["selem2"] = null;
                Session["selna2"] = "selected";
            }
            else if (example2 == "detail")
            {
                Session["selid2"] = null;
                Session["selna2"] = null;
                Session["selem2"] = null;
                Session["selem2"] = "selected";
            }
            if (sort == "time")
            {
                Session["time"] = null;
                Session["day"] = null;
                Session["tit"] = null;
                Session["time"] = "selected";
            }
            else if (sort == "day")
            {
                Session["time"] = null;
                Session["day"] = null;
                Session["tit"] = null;
                Session["day"] = "selected";
            }
            else if (sort == "tit")
            {
                Session["time"] = null;
                Session["day"] = null;
                Session["tit"] = null;
                Session["tit"] = "selected";
            }
            if (acde == "asc")
            {
                Session["asc"] = null;
                Session["des"] = null;
                Session["asc"] = "selected";
            }
            else if (acde == "des")
            {
                Session["asc"] = null;
                Session["des"] = null;
                Session["des"] = "selected";

            }

            try
            {
                string comand = "";
                if (string.IsNullOrEmpty(serch2))
                {
                    if ((example1 == "id") || (example1 == "title") || (example1 == "detail"))
                    {
                        if ((example1 == "id") && r1)
                        {
                            comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'";
                        }
                        else if ((example1 == "title") || (example1 == "detail"))
                        {
                            comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'";
                        }
                        else
                        {
                            ViewBag.error = "IDを数値以外で検索することはできません";
                        }
                        using (MySqlConnection conn = new MySqlConnection(hp))
                        {
                            conn.Open();
                            using (MySqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = $"select * from exhibit {comand} ORDER BY id ASC;";
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        AcList.Add(new userModel { id = int.Parse(reader["id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), price = reader["price"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                                    }
                                }
                            }
                        }
                        if (sort == "time")
                        {
                            if (acde == "asc") AcList.Sort((a, b) => a.time.CompareTo(b.time));
                            else AcList.Sort((a, b) => b.time.CompareTo(a.time));
                            return View(AcList);
                        }
                        else if (sort == "day")
                        {
                            if (acde == "asc") AcList.Sort((a, b) => a.time.CompareTo(b.time));
                            else AcList.Sort((a, b) => b.time.CompareTo(a.time));
                            return View(AcList);
                        }
                        else
                        {
                            if (acde == "asc") AcList.Sort((a, b) => a.title.CompareTo(b.title));
                            else AcList.Sort((a, b) => b.title.CompareTo(a.title));
                            return View(AcList);
                        }
                    }
                }
                else
                {
                    if ((example1 == "id" && example2 == "id") || (example1 == "id" && example2 == "title") || (example1 == "id" && example2 == "detail") || (example1 == "title" && example2 == "id") || (example1 == "detail" && example2 == "id"))
                    {
                        if ((example1 == "id" && r1) && (example2 == "id" && r2))
                            comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%'";
                        else if ((example1 == "title") || (example1 == "detail") || (example2 == "title") || (example2 == "detail"))
                            comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%'";
                        else
                            ViewBag.error = "IDを数値以外で検索することはできません";
                    }
                    if ((example1 == "title" && example2 == "title") || (example1 == "title" && example2 == "detail") || (example1 == "detail" && example2 == "title") || (example1 == "detail" && example2 == "detail"))
                        comand = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%'";

                    using (MySqlConnection conn = new MySqlConnection(hp))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = $"select * from exhibit {comand} ORDER BY id ASC;";
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    AcList.Add(new userModel { id = int.Parse(reader["id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), price = reader["price"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                                }
                            }
                        }
                    }

                    if (sort == "time")
                    {
                        if (acde == "asc") AcList.Sort((a, b) => a.time.CompareTo(b.time));
                        else AcList.Sort((a, b) => b.time.CompareTo(a.time));
                        return View(AcList);
                    }
                    else if (sort == "day")
                    {
                        if (acde == "asc") AcList.Sort((a, b) => a.time.CompareTo(b.time));
                        else AcList.Sort((a, b) => b.time.CompareTo(a.time));
                        return View(AcList);
                    }
                    else
                    {
                        if (acde == "asc") AcList.Sort((a, b) => a.title.CompareTo(b.title));
                        else AcList.Sort((a, b) => b.title.CompareTo(a.title));
                        return View(AcList);
                    }
                }

                using (MySqlConnection conn = new MySqlConnection(hp))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"select * from exhibit ORDER BY id ASC;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AcList.Add(new userModel { id = int.Parse(reader["id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), price = reader["price"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                            }
                        }
                    }
                }
                if (sort == "time")
                {
                    if (acde == "asc") AcList.Sort((a, b) => a.time.CompareTo(b.time));
                    else AcList.Sort((a, b) => b.time.CompareTo(a.time));
                    return View(AcList);
                }
                else if (sort == "day")
                {
                    if (acde == "asc") AcList.Sort((a, b) => a.time.CompareTo(b.time));
                    else AcList.Sort((a, b) => b.time.CompareTo(a.time));
                    return View(AcList);
                }
                else
                {
                    if (acde == "asc") AcList.Sort((a, b) => a.title.CompareTo(b.title));
                    else AcList.Sort((a, b) => b.title.CompareTo(a.title));
                    return View(AcList);
                }
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        //ユーサー情報　リスト
        public ActionResult AcuserList(string imagePath)
        {

            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var userList = new List<userModel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(hp))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"select * from acuser ORDER BY id ASC;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userList.Add(new userModel { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), password = reader["password"].ToString(), mail = reader["email"].ToString(), remark = reader["remark"].ToString() });
                            }
                        }
                    }
                }
                return View(userList);
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        [HttpPost]
        public ActionResult AcuserList(userModel model)
        {
            return View();
        }

        public ActionResult AcDelete(int id, string title, string detail, string money, string price, DateTime time, DateTime lasttime)
        {
            try
            {
                ViewBag.id = id;
                ViewBag.titl = title;
                ViewBag.detail = detail;
                ViewBag.money = money;
                ViewBag.price = price;
                ViewBag.time = time;
                ViewBag.lasttime = lasttime;

                return View(GetUser(id));
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        //出品情報　削除
        [HttpPost]
        public ActionResult AcDelete(int id)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hpdel = new MySqlConnection(hp);
            hpdel.Open();
            try
            {
                string delstr = @"delete from exhibit where id = @id";
                MySqlCommand delid = new MySqlCommand(delstr, hpdel);
                MySqlParameter did = new MySqlParameter("@id", MySqlDbType.Int16, 5);
                did.Direction = ParameterDirection.Input;
                did.Value = id;
                delid.Parameters.Add(did);
                delid.ExecuteNonQuery();
                return RedirectToAction("AcList");
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);

                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
            finally
            {
                hpdel.Close();
            }
        }

        //ユーザー情報　登録
        [HttpGet]
        public ActionResult AcRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AcRegistration(userModel model)
        {
            try
            {
                string ihp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;

                using (MySqlConnection hpin = new MySqlConnection(ihp))
                {
                    hpin.Open();
                    using (MySqlCommand cmd = hpin.CreateCommand())
                    {
                        cmd.CommandText = @"select * from acuser where ( id = " + model.id + ")";
                        using (MySqlDataReader sel = cmd.ExecuteReader())
                        {
                            if (sel.Read() == true)
                            {
                                ModelState.AddModelError(string.Empty, "入力したユーザーIDは既に使用されています。");
                                return View();
                            }
                            else
                            {
                                sel.Close();
                                cmd.CommandText = @"insert ignore into acuser (id,name,password,email,remark) values(" + model.id + ",'" + model.name + "','" + model.password + "','" + model.mail + "','" + model.remark + "')";
                                cmd.ExecuteNonQuery();
                                return RedirectToAction("AcuserList");

                            }
                        }
                    }
                }
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        //ユーザー情報　削除
        public ActionResult AcuserDelete(int id, string name, string password, string mail, string remark)
        {
            try
            {
                ViewBag.id = id;
                ViewBag.name = name;
                ViewBag.password = password;
                ViewBag.mail = mail;
                ViewBag.remark = remark;

                return View();
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        [HttpPost]
        public ActionResult AcuserDelete(int id)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hpdel = new MySqlConnection(hp);
            hpdel.Open();
            try
            {
                string delstr = @"delete from acuser where id = @id";
                MySqlCommand delid = new MySqlCommand(delstr, hpdel);
                MySqlParameter did = new MySqlParameter("@id", MySqlDbType.Int16, 5);
                did.Direction = ParameterDirection.Input;
                did.Value = id;
                delid.Parameters.Add(did);
                delid.ExecuteNonQuery();
                return RedirectToAction("AcuserList");
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);

                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
            finally
            {
                hpdel.Close();
            }
        }

        //ユーザー情報　編集
        public ActionResult AcuserEdit(int id, string name, string password, string mail, string remark)
        {
            try
            {
                ViewBag.id = id;
                return View();
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        [HttpPost]
        public ActionResult AcuserEdit(userModel model)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hped = new MySqlConnection(hp);
            hped.Open();
            try
            {
                string edstr = @"update ignore acuser set id = " + model.id + ", name = '" + model.name + "', password = '" + model.password + "', email = '" + model.mail + "', remark = '" + model.remark + "'where id = @id;";
                MySqlCommand edit = new MySqlCommand(edstr, hped);
                MySqlParameter ed = new MySqlParameter("@id", MySqlDbType.Int16);
                ed.Direction = ParameterDirection.Input;
                ed.Value = model.id;
                edit.Parameters.Add(ed);
                edit.ExecuteScalar();
                hped.Close();
                return RedirectToAction("AcuserList");
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
            finally
            {
                hped.Close();
            }
        }

        //出品情報　編集
        public ActionResult AcEdit(int id, string title, string detail, string money, string price, DateTime time, DateTime lasttime)
        {
            try
            {
                ViewBag.id = id;
                return View(GetUser(id));
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        [HttpPost]
        public ActionResult AcEdit(userModel model, HttpPostedFileWrapper file, int id)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hped = new MySqlConnection(hp);
            hped.Open();
            try
            {
                var files = Request.Files;
                string path = System.IO.Path.GetFileName(file.FileName);
                if (file.FileName == "")
                {
                    file.SaveAs("C:\\UploadedFiles\\" + path);
                }
                else
                    ViewBag.imger = "ファイルを選んでください";

                DateTime nowtime = DateTime.Now;
                string edstr = @"update ignore exhibit set id = " + model.id + ", title = '" + model.title + "', detail = '" + model.detail + "', money = '" + model.money + "', price = '" + model.price + "', time ='" + nowtime + "', lasttime ='" + nowtime.AddDays(7) + "', image = load_file(" + "'C:/UploadedFiles/" + path + "')" + "where id = @id;";

                MySqlCommand edit = new MySqlCommand(edstr, hped);
                MySqlParameter ed = new MySqlParameter("@id", MySqlDbType.Int16);
                ed.Direction = ParameterDirection.Input;
                ed.Value = model.id;
                edit.Parameters.Add(ed);
                edit.ExecuteScalar();
                hped.Close();
                return RedirectToAction("AcList");
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
            finally
            {
                hped.Close();
            }
        }

        public ActionResult AcDetails(int id, string title, string detail, string money, string price, DateTime time, DateTime lasttime)
        {
            try
            {
                ViewBag.id = id;
                ViewBag.titl = title;
                ViewBag.detail = detail;
                ViewBag.money = money;
                ViewBag.price = price;
                ViewBag.time = time;
                ViewBag.lasttime = lasttime;

                return View(GetUser(id));
            }
            catch (System.Exception er)
            {
                byte[] error;
                using (System.IO.MemoryStream ms = new MemoryStream())
                using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    string raw = er.ToString();
                    sw.WriteLine(raw);
                    sw.Flush();
                    error = ms.ToArray();
                }
                return File(error, "text", "error.txt");
            }
        }

        [HttpPost]
        public ActionResult AcDetails(userModel model)
        {
            return View();
        }

        //入札情報入力
        public ActionResult Acbid()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Acbid(userModel model)
        {
            return View();
        }

    }
}