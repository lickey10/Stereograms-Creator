using UnityEngine;
using System.Collections;

public class WebCamScript : MonoBehaviour {

	public string deviceName;
	WebCamTexture wct;
	
	
	// Use this for initialization
	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		deviceName = devices[0].name;
		wct = new WebCamTexture(deviceName, 400, 300, 12);
		GetComponent<Renderer>().material.mainTexture = wct;
		wct.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) 
		{
			//transform.rotate(
			TakePhoto();
		}
	}

	void TakePhoto()
	{
		Texture2D photo = new Texture2D(wct.width, wct.height);
		photo.SetPixels(wct.GetPixels());
		photo.Apply();
	
		
		//Encode to a PNG
		//byte[] bytes = photo.EncodeToPNG();
		//Write out the PNG. Of course you have to substitute your_path for something sensible
		//File.WriteAllBytes(your_path + "photo.png", bytes);
		
		StaticVariables.Picture = photo;
		
		wct.Stop();

		//this works
		//Application.LoadLevel("main");





		Application.LoadLevel("displayImage");
	}
}
