using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace TestCropImage
{
    public class ImageHandle
    {
        public static void ImageOver(string sAvata, string sName, string sbackground, string spathOut, int ix, int iy, int iw, int ih)
        {
            Bitmap bitMapImage = new Bitmap(sbackground);
            Graphics graphicImage = Graphics.FromImage(bitMapImage);
            Image image = new Bitmap(sAvata);

            Point[] myPoints = new Point[7];

            myPoints[0] = new Point(15, 20);
            myPoints[1] = new Point(25, 15);
            myPoints[2] = new Point(35, 20);
            myPoints[3] = new Point(35, 40);
            myPoints[4] = new Point(25, 45);
            myPoints[5] = new Point(15, 40);
            myPoints[6] = new Point(15, 20);
            
            //Bitmap newbitMap = ExtractPolygonAreaOfBitmap(ref bitMapImage,ref myPoints);
            Bitmap newbitMap = ExtractEllipAreaOfBitmap(ref bitMapImage,200,100,200,100);

            newbitMap.Save("d:\\ok.png", ImageFormat.Png);

            //Rectangle destRect1 = new Rectangle(ix, iy, iw, ih);
            //graphicImage.DrawImage(image, destRect1, 0, 0, iw, ih, GraphicsUnit.Pixel);
            ////graphicImage.DrawEllipse(image, destRect1, 0, 0, iw, ih, GraphicsUnit.Pixel);
            //string sPathOut = spathOut + "/" + sName + ".jpg";
            //bitMapImage.Save(sPathOut, ImageFormat.Jpeg);
            //graphicImage.Dispose();
            //bitMapImage.Dispose();
            //return sPathOut;
        }

        private static Bitmap ExtractPolygonAreaOfBitmap(ref Bitmap b, ref Point[] pts)
        {

            // A GraphicsPath will allow us to clip the output
            GraphicsPath gp = new GraphicsPath();

            // the path should be composed of the polygon
            gp.AddPolygon(pts);

            // ask the path to tell us how big the bounding rectangle is
            // we'll need that later to construct a new bitmap of the right size
            RectangleF rc = gp.GetBounds();

            // and it needs to be translated to the origin for clipping
            Matrix m = new Matrix(1, 0, 0, 1, -rc.X, -rc.Y);

            // Now we'll have a clipping path ready to use
            gp.Transform(m);

            // Create a new bitmap the same size as the polygon area
            Bitmap bmp = new Bitmap(Convert.ToInt32(rc.Width), Convert.ToInt32(rc.Height));

            // We need a Graphics so we can draw on our new bitmap
            Graphics g = Graphics.FromImage(bmp);

            // Initialize the new bitmap to some color (tranparent? white? your choice)
            g.Clear(Color.Transparent);

            // We're going to need a target rectangle too
            RectangleF rcDraw = new RectangleF(0, 0, rc.Width, rc.Height);

            // set up the clipping
            g.Clip = new Region(gp);

            // all set - now draw the clipped image
            g.DrawImage(b, rcDraw, rc, GraphicsUnit.Pixel);

            // clean up
            gp.Dispose();
            g.Dispose();

            // return the result
            return bmp;

        }

        private static Bitmap ExtractEllipAreaOfBitmap(ref Bitmap b,int x,int y,int w,int h)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(x,y,w,h);
            RectangleF rc = gp.GetBounds();
            Matrix m = new Matrix(1, 0, 0, 1, -rc.X, -rc.Y);
            gp.Transform(m);
            Bitmap bmp = new Bitmap(Convert.ToInt32(rc.Width), Convert.ToInt32(rc.Height));
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            RectangleF rcDraw = new RectangleF(0, 0, rc.Width, rc.Height);
            g.Clip = new Region(gp);
            g.DrawImage(b, rcDraw, rc, GraphicsUnit.Pixel);
            gp.Dispose();
            g.Dispose();
            return bmp;
        }
    }
}
