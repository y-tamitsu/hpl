using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.SessionState;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace hpl.Models
{
    public class userModel
    {
        [RegularExpression(@"[0-9]+", ErrorMessage = "半角数字のみ入力できます。")]
        public int id { get; set; }
        [RegularExpression(@"[a-zA-Z0-9 -/:-@\[-\`\{-\~]+", ErrorMessage = "半角英数字記号のみ入力できます。")]
        public string password { get; set; }
        public string name { get; set; }
        [RegularExpression(@"[\w!#$%&'*+/=?^_@{}\\|~-]+(\.[\w!#$%&'*+/=?^_{}\\|~-]+)*@([\w][\w-]*\.)+[\w][\w-]*", ErrorMessage = "正しいメールアドレスではありません。")]
        public string mail { get; set; }
        public string remark { get; set; }
        public HttpSessionState Session { get; }

        public string title { get; set; }

        public string detail { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "半角数字のみ入力できます。")]
        public string money { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "半角数字のみ入力できます。")]
        public string price { get; set; }

        public DateTime time { get; set; }

        public DateTime lasttime { get; set; }

        public string image { get; set; }
    }
}