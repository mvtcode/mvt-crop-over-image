using System;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using SD = System.Drawing;

namespace JCorpExample
{
    public partial class Crop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            joinImage(Server.MapPath("/images/") + "anh.jpg", Server.MapPath("/images/") + "1.png", HttpContext.Current.Request.PhysicalApplicationPath + "out.jpg");
        }

        private void joinImage(string ImgBg, string ImgFrame, string ImgOut)
        {
            try
            {
                int x1 = Convert.ToInt32(X.Value);
                int y1 = Convert.ToInt32(Y.Value);
                int w1 = Convert.ToInt32(W.Value);
                int h1 = Convert.ToInt32(H.Value);
                int w0 = Convert.ToInt32(W0.Value);
                int h0 = Convert.ToInt32(H0.Value);
                //load ảnh ghép
                System.Drawing.Image img = Bitmap.FromFile(ImgBg);
                img = ResizeImage(img, w1, h1);
                img = CropImg(img, w0, h0, x1, y1);
                Bitmap bmp = new Bitmap(w0, h0, img.PixelFormat);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(img, new Rectangle(0, 0, w0, h0));
                //Load frame => 0k
                System.Drawing.Image imageFrame = ResizeImage(Bitmap.FromFile(ImgFrame), w0, h0);
                g.DrawImage(imageFrame, new Rectangle(0, 0, w0, h0));
                bmp.Save(ImgOut, img.RawFormat);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (OutOfMemoryException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image sourceImage, int width, int height)
        {
            System.Drawing.Image oThumbNail = new Bitmap(sourceImage, width, height);
            Graphics oGraphic = Graphics.FromImage(oThumbNail);
            oGraphic.CompositingQuality = CompositingQuality.HighQuality;
            oGraphic.SmoothingMode = SmoothingMode.HighQuality;
            oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle oRectangle = new Rectangle(0, 0, width, height);
            oGraphic.DrawImage(sourceImage, oRectangle);
            return oThumbNail;
        }

        static System.Drawing.Image CropImg(SD.Image OriginalImage, int Width, int Height, int X, int Y)
        {
            try
            {
                Bitmap bmp = new Bitmap(Width, Height);
                bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                Graphics Graphic = SD.Graphics.FromImage(bmp);
                Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
                return (System.Drawing.Image)bmp;
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
    }
}
