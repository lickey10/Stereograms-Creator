using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject StereogramCube;
	public GameObject PictureCube;
	public GameObject PictureTitle;

	int currentImage = -1;
	Object[] images; 
	Renderer stereogramRender;
	Renderer pictureRender;

	// Use this for initialization
	void Start () {
		stereogramRender = StereogramCube.GetComponent<Renderer>();
		pictureRender = PictureCube.GetComponent<Renderer>();

		try 
		{
			images = Resources.LoadAll("Stereograms");

			NextPicture();
		} 
		catch (System.Exception ex) 
		{
			PictureTitle.GetComponent<UILabel>().text = ex.Message;
		}
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnSliderChange (float val)
	{
		stereogramRender.material.color = new Color(stereogramRender.material.color.r,stereogramRender.material.color.g,stereogramRender.material.color.b,val);
	}

	void NextPicture()
	{
		//set images
		currentImage++;

		if(images != null && images.Length > 0)
		{
			if(currentImage > images.Length - 1)
				currentImage = 0;

			//set title
			PictureTitle.GetComponent<UILabel>().text = images[currentImage].name;

			pictureRender.material.mainTexture = (Texture)images[currentImage];

			currentImage++;

			stereogramRender.material.mainTexture = (Texture)images[currentImage];
		}
		else
		{
			PictureTitle.GetComponent<UILabel>().text = "No Images found";
		}
	}

	//change the hue of a picture
//	// example 1
//	HSBColor hsbCol1=new HSBColor(renderer.material.color);
//	// example 2
//	HSBColor hsbCol2=HSBColor.FromColor(renderer.material.color);
//	
//	// example: clamp saturation
//	if(hsbCol2.s>0.5f)
//		hsbCol2.s=0.5;
//	
//	// convert back to Color, assign
//	renderer.material.color=hsbCol2.ToColor();


}
