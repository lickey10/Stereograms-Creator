using UnityEngine;
using System.Collections;

public class TakePicture : MonoBehaviour {

	WebCamTexture webCamTexture;
	
	void Start() 
	{
		StaticVariables.Picture = null;
		webCamTexture = new WebCamTexture();
		GetComponent<Renderer>().material.mainTexture = webCamTexture;
		webCamTexture.Play();
	}
	
	void TakePhoto()
	{
		Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
		photo.SetPixels(webCamTexture.GetPixels());
		photo.Apply();
		
		//Encode to a PNG
		//byte[] bytes = photo.EncodeToPNG();
		//Write out the PNG. Of course you have to substitute your_path for something sensible
		//File.WriteAllBytes(your_path + "photo.png", bytes);

		StaticVariables.Picture = photo;

		webCamTexture.Stop();

		Application.LoadLevel("main");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) 
		{

			TakePhoto();
		}
	}
}
