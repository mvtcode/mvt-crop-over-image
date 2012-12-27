using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestCropImage
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageHandle.ImageOver(Server.MapPath("/images/avata.jpg"), "", Server.MapPath("/images/a.jpg"), "", 1, 2, 3,
                                  4);
        }
    }
}
