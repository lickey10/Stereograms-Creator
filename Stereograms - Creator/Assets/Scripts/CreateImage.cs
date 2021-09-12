using UnityEngine;
using System.Collections;
//using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

public class CreateImage : MonoBehaviour {
    public Texture2D PictureToConvert;
    public Renderer stereogramRender;
    // Use this for initialization
    void Start () {
        //PictureToConvert = Resources.Load("LevelBitmaps/Level") as Bitmap;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateStereogram()
    {
        //fileName = txtFileName.Text;
        //Bitmap bitmap = new Bitmap(fileName);
        RDS(PictureToConvert, 90);
    }

    private void RDS(Texture2D bitmap, int stripWidth)
    {
        Random rnd = new Random();
        float randomDotColour;
        int  width, height;
        float scaling;

        //bitmap = new Bitmap(bitmap, new Size(bitmap.Width / 4, bitmap.Height / 4));
        bitmap = new Texture2D(750, 750);
        Texture2D imageAsset = new Texture2D(750, 750);
        //using (Graphics g = Graphics.FromImage(bitmap))
        //{
        //    g.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, bitmap.Width, bitmap.Height);

        //    bitmap.Save(@"D:\\utilities\\Programming\\StereoGram-HiddenImage\\StereoGram-HiddenImage\\backgroundColorTest.gif", System.Drawing.Imaging.ImageFormat.Gif);
        //}

        //bitmap = MakeGrayscale(bitmap);
        //bitmap.Save("D:\\utilities\\Programming\\StereoGram-HiddenImage\\StereoGram-HiddenImage\\grayscaleTest.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

        width = bitmap.width;
        height = bitmap.height;

        scaling = ((float)stripWidth / (float)5) / (float)255; // This makes sure full white (high points are white and have a value of 255) are always 1/5th the width of the image strip.

        //Draw the random strip
        for (int y = 0; y < height; y++)
            for (int x = 0; x < stripWidth; x++)
            {
                randomDotColour = Random.Range(0,1) * 255; // Random number,either 0 or 255
                UnityEngine.Color tempColor = new UnityEngine.Color(randomDotColour, randomDotColour, randomDotColour);

                bitmap.SetPixel(x, y,tempColor);
                //imageAsset.SetPixels32(x, y,1,1, Color32[);
                //System.Drawing.Color color = System.Drawing.Color.FromArgb(0, (float)randomDotColour, (float)randomDotColour, (float)randomDotColour);
                print("color");
            }

        //Create the RDS
        for (int x = stripWidth; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                bitmap.SetPixel(x, y, bitmap.GetPixel(x - stripWidth + (int)(scaling * bitmap.GetPixel(x, y).grayscale), y));
                //imageAsset.SetPixels32(x, y,1,1, bitmap.GetPixel(x - stripWidth + (int)(scaling * bitmap.GetPixel(x, y).R), y));
            }

        string filePath = Application.dataPath + "/" + "SavedImage";

        //bitmap.Save(filePath);

        //Texture2D tex = null;
        //byte[] fileData;

        //if (File.Exists(filePath))
        //{
        //    fileData = File.ReadAllBytes(filePath);
        //    tex = new Texture2D(2, 2);
        //    tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        //}


        stereogramRender.material.SetTexture(0, bitmap);


    //Texture2D stereogramImage = new Texture2D(90,90);
    //stereogramImage.SetPixels32(bitmap);

        //stereogramRender.material.mainTexture = bitmap;

        //Send the picture back to the browser
        //Response.ContentType = "image/gif";
        //bitmap.Save(Response.OutputStream, ImageFormat.Gif);

        //fileName = "D:\\utilities\\Programming\\StereoGram-HiddenImage\\StereoGram-HiddenImage\\StereoGram-dolphin_cutout_negative.gif";
        //fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "_StereoGram.gif";

        //bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
        //bitmap.Dispose();

        //picResult.Load(fileName);
        
    }

    public static Texture2D MakeGrayscale(Texture2D original)
    {
        //create a blank bitmap the same size as original
        Texture2D newBitmap = new Texture2D(original.width, original.height);

        //get a graphics object from the new image
        //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newBitmap);

        //create the grayscale ColorMatrix
        //ColorMatrix colorMatrix = new ColorMatrix(
        //   new float[][] 
        //  {
        //     new float[] {.3f, .3f, .3f, 0, 0},
        //     new float[] {.59f, .59f, .59f, 0, 0},
        //     new float[] {.11f, .11f, .11f, 0, 0},
        //     new float[] {0, 0, 0, 1, 0},
        //     new float[] {0, 0, 0, 0, 1}
        //  });

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
        //g.DrawImage(original, new Rectangle(0, 0, original.width, original.height),
           //0, 0, original.width, original.height, GraphicsUnit.Pixel, attributes);

        //dispose the Graphics object
        //g.Dispose();
        return newBitmap;
    }
}
