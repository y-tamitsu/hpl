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
    //出力処理
    //public ActionResult Export(userModel model)
    //{
    //    string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
    //    try
    //    {
    //        var userList = new List<string>();
    //        string sqlWhere = "";

    //        if (Session["serch1"] != null)
    //        {
    //            sqlWhere = "where " + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%'";
    //        }

    //        using (MySqlConnection conn = new MySqlConnection(hp))
    //        {
    //            conn.Open();
    //            using (MySqlCommand cmd = conn.CreateCommand())
    //            {
    //                cmd.CommandText = $"select * from userdata {sqlWhere} ORDER BY id ASC;";

    //                using (MySqlDataReader reader = cmd.ExecuteReader())
    //                {
    //                    while (reader.Read())
    //                    {
    //                        userList.Add(reader["id"].ToString() + "," + reader["name"].ToString() + "," + reader["password"].ToString() + "," + reader["email"].ToString() + "," + reader["remark"].ToString());
    //                    }
    //                }
    //            }
    //        }

    //        byte[] csv;
    //        using (System.IO.MemoryStream ms = new MemoryStream())
    //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
    //        {
    //            foreach (string row in userList)
    //            {
    //                sw.WriteLine(row);
    //            }
    //            sw.Flush();
    //            csv = ms.ToArray();
    //        }
    //        return File(csv, "text/csv", "userdata.csv");


    //    }
    //    catch (System.Exception er)
    //    {
    //        byte[] error;
    //        using (System.IO.MemoryStream ms = new MemoryStream())
    //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
    //        {
    //            string raw = er.ToString();
    //            sw.WriteLine(raw);

    //            sw.Flush();
    //            error = ms.ToArray();
    //        }
    //        return File(error, "text", "error.txt");
    //    }
    //}

    //[HttpPost]
    //public ActionResult Import(userModel model)
    //{
    //    if (Request.Files.Count == 0)
    //    {
    //        ViewBag.error = "エラーです";
    //    }
    //    var file = Request.Files[0];

    //    file.SaveAs("C:\\Users\\y-tamitsu\\Desktop");

    //    Session["path"] = "C:\\Users\\y - tamitsu\\Desktop";
    //    string con = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
    //    MySqlConnection hped = new MySqlConnection(con);
    //    hped.Open();
    //    try
    //    {
    //        string exstr = @"load data local infile " + Session["path"] + " into table userdata fields terminated by ',';";
    //        MySqlCommand exit = new MySqlCommand(exstr, hped);
    //        MySqlParameter ex = new MySqlParameter("@id", MySqlDbType.Int64);
    //        ex.Direction = ParameterDirection.Input;
    //        ex.Value = model.id;
    //        exit.Parameters.Add(ex);
    //        exit.ExecuteNonQuery();
    //        hped.Close();
    //        return RedirectToAction("List");
    //    }

    //    catch (System.Exception er)
    //    {
    //        byte[] error;
    //        using (System.IO.MemoryStream ms = new MemoryStream())
    //        using (System.IO.StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("utf-8")))
    //        {
    //            string raw = er.ToString();
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

    //public ActionResult Error()
    //{
    //    ViewBag.ce = Session["error"];
    //    return View();
    //}
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //　ここからオークション

        // item selectメソッド
        public List<userModel> GetAcList()
        {
            //dbから値を取得
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var AcList = new List<userModel>();

            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from exhibit ORDER BY item_id ASC";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AcList.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                        }
                    }
                }
            }
            return AcList;
        }

        // id による select itemメソッド
        public userModel Getexhibit(int id)
        {
            //Where id = idのSelect
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new userModel();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from exhibit where ( item_id = " + id + ")";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), disc = int.Parse(reader["disc"].ToString()), image = System.Convert.ToBase64String((byte[])reader["image"]) };
                    return result;
                }
            }
        }

        public List<string> Getexuserid(int id)
        {
            //Where id = idのSelect
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new List<string>();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select item_id from bid where ( user_id = " + id + ")";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(reader["item_id"].ToString());
                    }
                    return result;
                }
            }
        }

        // title による select itemメソッド
        public userModel Getex_tit(DateTime time)
        {
            //Where id = idのSelect
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new userModel();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select item_id,money,lastprice from exhibit where ( time = '" + time + "')";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new userModel { id = int.Parse(reader["item_id"].ToString()), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString() };
                    return result;
                }
            }
        }

        // item id による select imageメソッド
        public userModel GetAc(int id)
        {
            //Where id = idのSelect
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new userModel();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select image from exhibit where ( item_id = " + id + ")";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new userModel { image = System.Convert.ToBase64String((byte[])reader["image"]) };
                    return result;
                }
            }
        }

        // item id による select timeメソッド
        public userModel Gettime(int id)
        {
            //Where id = idのSelect
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new userModel();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select time,lasttime from exhibit where item_id = " + id;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new userModel { time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]) };
                    return result;
                }
            }
        }
        // user id による select
        public userModel Getacuser(int id)
        {
            //Where id = idのSelect
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new userModel();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from acuser where user_id = " + id;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new userModel { id = int.Parse(reader["user_id"].ToString()), name = reader["name"].ToString(), password = reader["password"].ToString(), mail = reader["email"].ToString(), remark = reader["remark"].ToString() };
                    return result;
                }
            }
        }

        // user id による biddata
        public userModel Getuserbid(int id)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var result = new userModel();
            using (MySqlConnection conn = new MySqlConnection(hp))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from bid where user_id = " + id;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    result = (new userModel { id = int.Parse(reader["item_id"].ToString()), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]) });
                    return result;
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //ログイン
        public ActionResult AcLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AcLogin(userModel model, int id, string password)
        {
            try
            {
                string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
                Session["loginid"] = id;

                using (MySqlConnection conn = new MySqlConnection(hp))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT* FROM acuser where user_id = " + id + " and password = '" + password + "' ORDER BY user_id ASC;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                // ユーザー認証 成功
                                Session["loginid"] = model.id;
                                Session["username"] = model.name;
                                return Redirect("AcList");
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

                string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
                using (MySqlConnection hpin = new MySqlConnection(hp))
                {
                    using (MySqlCommand cmd = hpin.CreateCommand())
                    {
                        hpin.Open();

                        cmd.CommandText = @"insert ignore into exhibit (user_id,title,detail,money,lastprice,time,lasttime,image) values(" + int.Parse(Session["loginid"].ToString()) + ",'" + model.title + "','" + model.detail + "','" + model.money + "','" + model.lastprice + "','" + nowtime + "','" + weektime + "',load_file(" + "'C:/UploadedFiles/" + path + "'))";
                        cmd.ExecuteNonQuery();
                        return RedirectToAction("AcList");
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

        //出品情報　リスト
        public ActionResult AcList(userModel model)
        {
            Session["and"] = "checked";
            Session["bidend"] = "checked";
            Session["required"] = "required";
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            var AcList = new List<userModel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(hp))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"select * from exhibit ORDER BY item_id ASC;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AcList.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
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
        public ActionResult AcList(string select, string ex, string sort, string acde, string bidend, string example1, string example2, string serch1, string serch2)
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
            Session["bidend"] = bidend;
            Session["example1"] = example1;
            Session["example2"] = example2;
            Session["serch1"] = serch1;
            Session["serch2"] = serch2;

            if (serch1 == null)
            {
                Session["required"] = "required";
            }
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
            if (ex == "non")
            {
                Session["non"] = null;
                Session["all"] = null;
                Session["exh"] = null;
                Session["bid"] = null;
                Session["non"] = "selected";
            }
            else if (ex == "all")
            {
                Session["non"] = null;
                Session["all"] = null;
                Session["exh"] = null;
                Session["bid"] = null;
                Session["all"] = "selected";
            }
            else if (ex == "exh")
            {
                Session["non"] = null;
                Session["all"] = null;
                Session["exh"] = null;
                Session["bid"] = null;
                Session["exh"] = "selected";
            }
            else if (ex == "bid")
            {
                Session["non"] = null;
                Session["all"] = null;
                Session["exh"] = null;
                Session["bid"] = null;
                Session["bid"] = "selected";
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
            if (bidend == "bidend")
            {
                Session["bidend"] = null;
                Session["bidend"] = "checked";
            }
            else if (bidend == null)
            {
                Session["bidend"] = null;
            }

            try
            {
                if (ex == "non")
                {
                    string comand = "";
                    if (string.IsNullOrEmpty(serch2))
                    {
                        if ((example1 == "id") || (example1 == "title") || (example1 == "detail"))
                        {
                            if ((example1 == "id") && r1)
                            {
                                comand = "where (item_" + Session["example1"] + " like '%" + Session["serch1"] + "%')";
                            }
                            else if ((example1 == "title") || (example1 == "detail"))
                            {
                                comand = "where (" + Session["example1"] + " like '%" + Session["serch1"] + "%')";
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
                                    if (bidend == null)
                                    {
                                        cmd.CommandText = $"select * from exhibit {comand} and (disc = " + 1 + ") ORDER BY item_id ASC";
                                    }
                                    else
                                    {
                                        cmd.CommandText = $"select * from exhibit {comand} ORDER BY item_id ASC";
                                    }
                                    using (MySqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            AcList.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                                        }
                                    }
                                }
                            }
                            if (sort == "time")
                            {
                                if (acde == "asc") AcList.Sort((a, b) => a.lasttime.CompareTo(b.lasttime));
                                else AcList.Sort((a, b) => b.lasttime.CompareTo(a.lasttime));
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
                        if ((example1 == "id" && example2 == "title") || (example1 == "id" && example2 == "detail") || (example1 == "title" && example2 == "id") || (example1 == "detail" && example2 == "id"))
                        {
                            if (example1 == "id" && r1)
                                comand = "where (item_" + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%')";
                            else if (example2 == "id" && r2)
                                comand = "where (" + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " item_" + Session["example2"] + " like '%" + Session["serch2"] + "%')";
                            else if (example1 == "title" || example1 == "detail")
                                comand = "where (" + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " item_" + Session["example2"] + " like '%" + Session["serch2"] + "%')";
                            else if (example2 == "title" || example2 == "detail")
                                comand = "where (item_" + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%')";
                            else
                                ViewBag.error = "IDを数値以外で検索することはできません";
                        }
                        if ((example1 == "title" && example2 == "title") || (example1 == "title" && example2 == "detail") || (example1 == "detail" && example2 == "title") || (example1 == "detail" && example2 == "detail"))
                            comand = "where (" + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " " + Session["example2"] + " like '%" + Session["serch2"] + "%')";
                        if (example1 == "id" && example2 == "id")
                            comand = "where (item_" + Session["example1"] + " like '%" + Session["serch1"] + "%'" + Session["select"] + " item_" + Session["example2"] + " like '%" + Session["serch2"] + "%')";

                        using (MySqlConnection conn = new MySqlConnection(hp))
                        {
                            conn.Open();
                            using (MySqlCommand cmd = conn.CreateCommand())
                            {
                                if (bidend == null)
                                {
                                    cmd.CommandText = $"select * from exhibit {comand} and (disc = " + 1 + ") ORDER BY item_id ASC";
                                }
                                else
                                {
                                    cmd.CommandText = $"select * from exhibit {comand} ORDER BY item_id ASC";
                                }
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        AcList.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
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

                else if (ex == "exh")
                {
                    var result = new List<userModel>();
                    using (MySqlConnection conn = new MySqlConnection(hp))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            if (bidend == null)
                            {
                                cmd.CommandText = $"select * from exhibit where user_id = " + Session["loginid"] + " and (disc = " + 1 + ") ORDER BY item_id ASC";
                            }
                            else
                            {
                                cmd.CommandText = $"select * from exhibit where user_id = " + Session["loginid"] + " ORDER BY item_id ASC";
                            }
                            MySqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                result.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                            }
                        }
                    }
                    if (sort == "time")
                    {
                        if (acde == "asc") result.Sort((a, b) => a.time.CompareTo(b.time));
                        else result.Sort((a, b) => b.time.CompareTo(a.time));
                        return View(result);
                    }
                    else if (sort == "day")
                    {
                        if (acde == "asc") result.Sort((a, b) => a.time.CompareTo(b.time));
                        else result.Sort((a, b) => b.time.CompareTo(a.time));
                        return View(result);
                    }
                    else
                    {
                        if (acde == "asc") result.Sort((a, b) => a.title.CompareTo(b.title));
                        else result.Sort((a, b) => b.title.CompareTo(a.title));
                        return View(result);
                    }
                }
                else if (ex == "bid")
                {
                    var result = new List<userModel>();
                    List<string> item_id = Getexuserid(int.Parse(Session["loginid"].ToString()));

                    using (MySqlConnection conn = new MySqlConnection(hp))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            if (bidend == null)
                            {
                                foreach (string item in item_id)
                                {
                                    int it_id = Getexhibit(int.Parse(item)).id;
                                    cmd.CommandText = $"select * from exhibit where (item_id = " + it_id + ") and (disc = " + 1 + ") ORDER BY item_id ASC";
                                    MySqlDataReader reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        result.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                                    }
                                    reader.Close();
                                }
                            }
                            else
                            {
                                foreach (string item in item_id)
                                {
                                    int it_id = Getexhibit(int.Parse(item)).id;
                                    cmd.CommandText = $"select * from exhibit where item_id = " + it_id + " ORDER BY item_id ASC";
                                    MySqlDataReader reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        result.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
                                    }
                                    reader.Close();
                                }
                            }
                        }
                    }
                    if (sort == "time")
                    {
                        if (acde == "asc") result.Sort((a, b) => a.time.CompareTo(b.time));
                        else result.Sort((a, b) => b.time.CompareTo(a.time));
                        return View(result);
                    }
                    else if (sort == "day")
                    {
                        if (acde == "asc") result.Sort((a, b) => a.time.CompareTo(b.time));
                        else result.Sort((a, b) => b.time.CompareTo(a.time));
                        return View(result);
                    }
                    else
                    {
                        if (acde == "asc") result.Sort((a, b) => a.title.CompareTo(b.title));
                        else result.Sort((a, b) => b.title.CompareTo(a.title));
                        return View(result);
                    }
                }
                else //ex == all
                {
                    using (MySqlConnection conn = new MySqlConnection(hp))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            if (bidend == null)
                            {
                                cmd.CommandText = $"select * from exhibit where disc = " + 1 + " ORDER BY item_id ASC";
                            }
                            else
                            {
                                cmd.CommandText = $"select * from exhibit ORDER BY item_id ASC";
                            }
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    AcList.Add(new userModel { id = int.Parse(reader["item_id"].ToString()), title = reader["title"].ToString(), detail = reader["detail"].ToString(), money = reader["money"].ToString(), lastprice = reader["lastprice"].ToString(), time = Convert.ToDateTime(reader["time"]), lasttime = Convert.ToDateTime(reader["lasttime"]), image = System.Convert.ToBase64String((byte[])reader["image"]) });
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

        //出品情報　編集
        public ActionResult AcEdit(int id, string title, string detail, string money, string lastprice, DateTime time, DateTime lasttime)
        {
            try
            {
                ViewBag.id = id;
                ViewBag.time = time;
                ViewBag.lasttime = lasttime;
                return View(GetAc(id));
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
                var time = Gettime(id).time;
                var lasttime = Gettime(id).lasttime;

                if (file == null)
                {
                    string fnst = @"update ignore exhibit set title = '" + model.title + "', detail = '" + model.detail + "', money = '" + model.money + "', lastprice = '" + model.lastprice + "' where item_id = " + id;
                    MySqlCommand fnup = new MySqlCommand(fnst, hped);
                    fnup.ExecuteScalar();
                }
                else
                {
                    string path = System.IO.Path.GetFileName(file.FileName);
                    file.SaveAs("C:\\UploadedFiles\\" + path);
                    string edstr = @"update ignore exhibit set title = '" + model.title + "', detail = '" + model.detail + "', money = '" + model.money + "', lastprice = '" + model.lastprice + "', image = load_file(" + "'C:/UploadedFiles/" + path + "')" + " where item_id = " + id;
                    MySqlCommand edst = new MySqlCommand(edstr, hped);
                    edst.ExecuteScalar();
                }
                string edite = $"update bid set money = '" + model.money + "', lastprice = '" + model.lastprice + "' where item_id = " + id;
                MySqlCommand edit = new MySqlCommand(edite, hped);
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

        public ActionResult AcDetails(int id, string title, string detail, string money, string lastprice, DateTime time, DateTime lasttime)
        {
            try
            {
                ViewBag.id = id;
                ViewBag.titl = title;
                ViewBag.detail = detail;
                ViewBag.money = money;
                ViewBag.lastprice = lastprice;
                ViewBag.time = time;
                ViewBag.lasttime = lasttime;
                return View(GetAc(id));
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

        //出品情報　削除
        public ActionResult AcDelete(int id, string title, string detail, string money, string lastprice, DateTime time, DateTime lasttime)
        {
            try
            {
                ViewBag.id = id;
                ViewBag.titl = title;
                ViewBag.detail = detail;
                ViewBag.money = money;
                ViewBag.lastprice = lastprice;
                ViewBag.time = time;
                ViewBag.lasttime = lasttime;

                return View(GetAc(id));
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
        public ActionResult AcDelete(int id, string title)
        {
            string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
            MySqlConnection hpdel = new MySqlConnection(hp);
            hpdel.Open();
            try
            {
                string delbid = @"delete from bid where item_id = " + id;
                MySqlCommand debid = new MySqlCommand(delbid, hpdel);
                debid.ExecuteNonQuery();

                string delstr = @"delete from exhibit where item_id = " + id;
                MySqlCommand delid = new MySqlCommand(delstr, hpdel);
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                        cmd.CommandText = @"select * from acuser where ( user_id = " + model.id + ")";
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
                                cmd.CommandText = @"insert ignore into acuser (user_id,name,password,email,remark) values(" + model.id + ",'" + model.name + "','" + model.password + "','" + model.mail + "','" + model.remark + "')";
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
                        cmd.CommandText = $"select * from acuser ORDER BY user_id ASC;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userList.Add(new userModel { id = int.Parse(reader["user_id"].ToString()), name = reader["name"].ToString(), password = reader["password"].ToString(), mail = reader["email"].ToString(), remark = reader["remark"].ToString() });
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
                string edstr = @"update ignore acuser set user_id = " + model.id + ", name = '" + model.name + "', password = '" + model.password + "', email = '" + model.mail + "', remark = '" + model.remark + "' where user_id = " + model.id;
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
                string delstr = @"delete from acuser where user_id = @id";
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //入札情報入力
        public ActionResult Acbid(userModel model)
        {
            ViewBag.id = model.id;
            ViewBag.titl = model.title;
            ViewBag.money = model.money;
            ViewBag.lastprice = model.lastprice;
            ViewBag.time = model.time;
            ViewBag.lasttime = model.lasttime;
            return View(GetAc(int.Parse(model.id.ToString())));
        }

        [HttpPost]
        public ActionResult Acbid(int bidprice, int id, string money, string lastprice, string time, string lasttime)
        {
            try
            {
                string hp = ConfigurationManager.ConnectionStrings["Hp"].ConnectionString;
                int price = 0;
                money = Getexhibit(id).money;
                lastprice = Getexhibit(id).lastprice;
                DateTime nowtime = DateTime.Now;
                lasttime = Gettime(id).lasttime.ToString();
                Session["hope"] = bidprice;
                if (bidprice >= int.Parse(lastprice))
                {
                    //入札終了
                    price = int.Parse(lastprice);
                    using (MySqlConnection hpup = new MySqlConnection(hp))
                    {
                        using (MySqlCommand cmd = hpup.CreateCommand())
                        {
                            hpup.Open();
                            string dis = $"update exhibit set disc = " + 0 + " where item_id = " + id;
                            MySqlCommand disc = new MySqlCommand(dis, hpup);
                            disc.ExecuteNonQuery();
                        }
                    }
                }

                if (bidprice < int.Parse(money))
                {
                    price = bidprice + 100;
                }
                else if (bidprice > int.Parse(money))
                {
                    price = int.Parse(money) + 100;
                }
                else if (bidprice == int.Parse(money))
                {
                    price = bidprice;
                }

                using (MySqlConnection hpup = new MySqlConnection(hp))
                {
                    using (MySqlCommand cmd = hpup.CreateCommand())
                    {
                        hpup.Open();
                        cmd.CommandText = $"insert ignore into bid (item_id,user_id,money,lastprice,time,lasttime) values(" + id + "," + int.Parse(Session["loginid"].ToString()) + ",'" + money + "','" + lastprice + "','" + nowtime + "','" + lasttime + "')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = $"update bid set money = " + price + " where (user_id = " + Session["loginid"] + ") and (item_id = " + id + " )";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = $"update exhibit set money = " + price + " where item_id = " + id;
                        cmd.ExecuteNonQuery();
                        return RedirectToAction("AcList");
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
    }
}