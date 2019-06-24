using UnityEngine;
using System.Collections;
using System.IO;

public class CreateCube : MonoBehaviour {

	private ArrayList cubes;
	public Texture2D image;
	GUIStyle labelStyle;
	private string imageString = "";

	IEnumerator Start () {
		// Set low quality and faster time scale for smoother simulation
		QualitySettings.currentLevel = QualityLevel.Fastest;
		Time.timeScale = 2;
		
		labelStyle = new GUIStyle();
		labelStyle.normal.textColor = Color.black;
		
		cubes = new ArrayList();
		
		WWW www;
		
		if (Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer) {
			string su = Application.srcValue; 		
			string qs = su.Substring(su.IndexOf("?") + 1);
			char [] deli = "=".ToCharArray();
			string[] ps = qs.Split(deli);
			www = new WWW(ps[1]);
			yield return www;
			image = www.texture; 
		} else {
			if(StaticVariables.Picture != null)
				image = StaticVariables.Picture;
//			else if(image == null)
//			{
//				// Use this hardcoded path in editor for testing
//				www = new WWW("http://www.everyday3d.com/unity3d/cubepict/medusa.jpg");
//				yield return www;
//				image = www.texture;
//			}
		}
		
		image.wrapMode = TextureWrapMode.Clamp;
		
		float ratio = (float)image.width / (float)image.height;
		
		float ymh, xmw;

		if(ratio >= 1.2f) {
			ymh = 50.0f * (1.0f / ratio);
			xmw = ymh * ratio;
		} else if(ratio < 1.2f && ratio >= 1.0f) {
			ymh = 40.0f * (1.0f / ratio);
			xmw = ymh * ratio;
		} else {
			ymh = 40.0f;
			xmw = ymh * ratio;
		}
		
		for(int ym = 0; ym < ymh; ym++) {
			for(int xm = 0; xm < xmw; xm++) {

				Color c = image.GetPixelBilinear(xm/xmw, ym/ymh);
				if(c.r > 0.95f && c.g > 0.95f && c.b > 0.95f) continue; // Ignore white and very light pixels

				//the cube creation
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//cube.transform.position = new Vector3(xm-xmw/2, ym-ymh/2, 0);
				cube.transform.position = new Vector3(xm-xmw/2, ym, 0);
				cube.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
				//cube.transform.rotation = Random.rotation;
				cube.GetComponent<Renderer>().material.color = c;
				//cube.renderer.material.color = c.grayscale;
				cube.GetComponent<Renderer>().castShadows = false;
				cube.GetComponent<Renderer>().receiveShadows = false;
				cubes.Add(cube);


				float grayScale = c.grayscale;

				if(grayScale > .5f)
					imageString += "&";
				else
					imageString += "!";


			}
		}
		
		
//		 foreach(GameObject cube in cubes) {
//			cube.AddComponent<RollOverCube>();
//		}

		foreach(GameObject cube in cubes) {
						cube.AddComponent<Block>();
					}
	}
	
	void OnGUI () {
		//GUI.Label(new Rect(10, 10, 400, 24), "Rollover the image to decompose it.", labelStyle);
	}
	
	// Save the snapshot. This doesn't work in web player (but would be cool if it did!).
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			int sw = Screen.width;
			int sh = Screen.height;
			Texture2D tex = new Texture2D (sw, sh, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);
			tex.Apply();
			byte[] bytes = tex.EncodeToPNG();
			Destroy(tex);
			File.WriteAllBytes("/Users/Desktop/shot.png", bytes);
		}
	}
}
