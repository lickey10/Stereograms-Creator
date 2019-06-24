using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Drawing;

public class DisplayImageScript : MonoBehaviour {

	UnityEngine.UI.Text txtResults;
    public Texture2D picture;
    public GameObject ThePicture;
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
            if (StereogramCube == null)
                StereogramCube = GameObject.FindGameObjectWithTag("ImageDisplay");

            stereogramRender = StereogramCube.GetComponent<Renderer>();
            Texture2D theImage = new Texture2D(750, 750);
            byte[] theImageArray = MakeStereograms2.Stereograms.RDS(picture.EncodeToPNG(), 90, "test.gif");

            //save image to file
            //var path = System.IO.Path.Combine(Application.persistentDataPath, "stereogramTest_" + ".png");
            var path = "assets\\resources\\stereograms\\temp_stereogram.png";
            File.WriteAllBytes(path, theImageArray);
            images = Resources.LoadAll("Stereograms");

            //Texture2D text = new Texture2D(1, 1);
            //text.LoadImage(theImageArray);

            
            //SpriteRenderer spriteRenderer = GameObject.FindGameObjectWithTag("sprite").GetComponent<SpriteRenderer>();
            
            //Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
            //spriteRenderer.sprite = sprite; //Image is a defined reference to an image component

            foreach (Texture texture in images)
            {
                if (texture.name == "temp_stereogram")
                {
                    stereogramRender.material.mainTexture = texture;
                    break;
                }
            }

            File.Delete(path);
        }
    }
    
	public void Update()
	{

	}

    //this was throwing error and not used so I commented it out
    //private static Bitmap byteArrayToImage(byte[] byteArrayIn)
    //{
    //    MemoryStream ms = new MemoryStream(byteArrayIn);
    //    Bitmap returnImage = new Bitmap(ms);
    //    return returnImage;
    //}
}
