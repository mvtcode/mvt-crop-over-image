using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace JCorpExample
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Submit_Click(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                //Get the Cordinates
                int x = Convert.ToInt32(X.Value);
                int y = Convert.ToInt32(Y.Value);
                int w = Convert.ToInt32(W.Value);
                int h = Convert.ToInt32(H.Value);

                //Load the Image from the location
                System.Drawing.Image image = Bitmap.FromFile(
                    HttpContext.Current.Request.PhysicalApplicationPath + "Sunset.jpg");

                //Create a new image from the specified location to
                //specified height and width
                Bitmap bmp = new Bitmap(w, h, image.PixelFormat);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(image, new Rectangle(0, 0, w, h), 
                    new Rectangle(x, y, x + w, y + h), GraphicsUnit.Pixel);

                //Save the file and reload to the control
                bmp.Save(HttpContext.Current.Request.PhysicalApplicationPath + "Sunset2.jpg", image.RawFormat);
                //cropedImage.Visible = true;
                //cropedImage.ImageUrl = ".\\Sunset2.jpg";
            }
        }
    }
}
