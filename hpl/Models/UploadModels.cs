using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace hpl.Models
{
    public class UploadModels
    {
        [DisplayName("ファイル入力")]
        public HttpPostedFileBase UploadFile { get; set; }

    }
}