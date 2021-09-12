using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
//using System.Drawing;

public class DisplayImageScript : MonoBehaviour {

	UnityEngine.UI.Text txtResults;
    public Texture2D picture;
    //public GameObject ThePicture;
    public GameObject StereogramCube;
    Renderer stereogramRender;
    //public Image NewImage;
    Object[] images;

    // Use this for initialization
    void Start () {
		txtResults = GetComponent<Text>();

        //ConvertToAscii (StaticVariables.Picture);

        if (StaticVariables.Picture != null)
        {
            picture = StaticVariables.Picture;//get the picture from camera
        }
        
        stereogramRender = StereogramCube.GetComponent<Renderer>();

        if (picture != null)
        {
            saveImageToFile(picture, "stereogram0");

            if (StereogramCube == null)
                StereogramCube = GameObject.FindGameObjectWithTag("ImageDisplay");
            
            stereogramRender = StereogramCube.GetComponent<Renderer>();

            saveImageToFile(MakeGrayscale(picture), "stereogram0");

            //stereogramRender.material.mainTexture = createStereogram(picture, 90);
            Texture2D newPicture = new Texture2D(picture.width, picture.height);
            newPicture.LoadImage(Stereograms.CreateStereogram(picture.EncodeToPNG(), 90));
            stereogramRender.material.mainTexture = newPicture;


        }
    }

    private void saveImageToFile(Texture2D imageToSave, string fileName)
    {
        Texture2D updatedImage = new Texture2D(imageToSave.width, imageToSave.height);
        updatedImage.LoadImage(imageToSave.EncodeToPNG());
        updatedImage.Apply();

        //first Make sure you're using RGB24 as your texture format
        //Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        ////then Save To Disk as PNG
        //byte[] bytes = texture.EncodeToPNG();
        byte[] bytes = updatedImage.EncodeToPNG();

        var dirPath = Application.dataPath + "/../SaveImages/";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllBytes(dirPath + fileName + ".png", bytes);
    }

    private Texture2D createStereogram(Texture2D imageToConvert, int stripWidth)
    {
        int randomDotColour, width, height;
        float scaling;
        //Texture2D returnImage = new Texture2D(imageToConvert.width, imageToConvert.height);
        //returnImage.LoadImage(imageToConvert.EncodeToPNG());

        //returnImage.Resize(750, 750);
        //returnImage.Apply();

        Texture2D returnImage = MakeGrayscale(imageToConvert);
        //saveImageToFile(returnImage, "stereogram1");
        width = returnImage.width;
        height = returnImage.height;

        scaling = ((float)stripWidth / (float)5) / (float)255; // This makes sure full white (high points are white and have a value of 255) are always 1/5th the width of the image strip.

        //Draw the random strip
        for (int y = 0; y < height; y++)
            for (int x = 0; x < stripWidth; x++)
            {
                randomDotColour = Random.Range(0, 2) * 255; // Random number,either 0 or 255
                returnImage.SetPixel(x, y, new Color(randomDotColour, randomDotColour, randomDotColour,0));
            }

        //returnImage.Apply();

        //saveImageToFile(returnImage, "stereogram2");
        //Create the RDS
        for (int x = stripWidth; x < width; x++)
            for (int y = 0; y < height/2; y++)
            {
                returnImage.SetPixel(x, y, returnImage.GetPixel(x - stripWidth + (int)(scaling * (imageToConvert.GetPixel(x, y).r)), y));

            }

        returnImage.Apply();
        //saveImageToFile(returnImage, "stereogram3");
        return returnImage;
    }

    private Texture2D MakeGrayscale(Texture2D tex)
    {
        Texture2D returnImage = new Texture2D(tex.width, tex.height);
        returnImage.LoadImage(tex.EncodeToPNG());
        var texColors = returnImage.GetPixels();

        for (int i = 0; i < texColors.Length; i++)
        {
            var grayValue = texColors[i].grayscale;
            texColors[i] = new Color(grayValue, grayValue, grayValue, texColors[i].a);
        }

        returnImage.SetPixels(texColors);
        returnImage.Apply();

        return returnImage;
    }


    public void Update()
	{

	}
}
