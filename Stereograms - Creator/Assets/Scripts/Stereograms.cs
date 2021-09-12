using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.IO;

public class Stereograms
{
    public static byte[] CreateStereogram(byte[] bitmapArray, int stripWidth)
    {
        Bitmap bitmap = byteArrayToImage(bitmapArray);
        Random rnd = new Random();
        int randomDotColour, width, height;
        float scaling;

        //bitmap = new Bitmap(bitmap, new Size(750, 750));

        bitmap = MakeGrayscale(bitmap);

        width = bitmap.Width;
        height = bitmap.Height;

        scaling = ((float)stripWidth / (float)5) / (float)255; // This makes sure full white (high points are white and have a value of 255) are always 1/5th the width of the image strip.

        //Draw the random strip
        for (int y = 0; y < height; y++)
            for (int x = 0; x < stripWidth; x++)
            {
                randomDotColour = rnd.Next(0, 2) * 255; // Random number,either 0 or 255
                bitmap.SetPixel(x, y, Color.FromArgb(0, randomDotColour, randomDotColour, randomDotColour));
            }
        
        //Create the RDS
        for (int x = stripWidth; x < width; x++)
            for (int y = 0; y < height; y++)
                bitmap.SetPixel(x, y, bitmap.GetPixel(x - stripWidth + (int)(scaling * bitmap.GetPixel(x, y).R), y));

        return imageToByteArray(bitmap);
    }

    private static byte[] imageToByteArray(Bitmap imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return ms.ToArray();
    }

    private static Bitmap byteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Bitmap returnImage = new Bitmap(ms);
        return returnImage;
    }

    private static Bitmap MakeGrayscale(Bitmap original)
    {
        //create a blank bitmap the same size as original
        Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //get a graphics object from the new image
        Graphics g = Graphics.FromImage(newBitmap);

        ColorMatrix colorMatrix = new ColorMatrix(
           new float[][]
          {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
          });

        //create some image attributes
        ImageAttributes attributes = new ImageAttributes();

        //set the color matrix attribute
        attributes.SetColorMatrix(colorMatrix);

        //draw the original image on the new image
        //using the grayscale color matrix
        g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

        //dispose the Graphics object
        g.Dispose();
        return newBitmap;
    }
}

