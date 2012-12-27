using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestCropImage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            //ListDirectories(Server.MapPath("/"),ref sb);
            ListDirectories(@"D:\Project\", ref sb);
            Response.Clear();
            Response.Write(sb.ToString());
            Response.End();
        }
        void ListDirectories(string path,ref StringBuilder sb)
        {
            var directories = Directory.GetDirectories(path);
            if (directories.Any())
            {
                sb.AppendLine("<ul>");
                foreach (var directory in directories)
                {
                    var di = new DirectoryInfo(directory);
                    sb.AppendFormat("<li>{0}</li>", di.Name);
                    sb.AppendLine();
                    ListDirectories(directory,ref sb);
                }
                sb.AppendLine("</ul>");
            }
        }
    }
}
