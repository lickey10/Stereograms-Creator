using UnityEngine;
using System.Collections;


public class CreateWeapon : MonoBehaviour {

	public GameObject Weapon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

//		if (Input.GetMouseButtonDown(0))
//		{
//			var ray = camera.ScreenPointToRay (Input.mousePosition);
//			var hit : RaycastHit;
//			
//			if (Physics.Raycast (ray, hit, Mathf.Infinity)) 
//			{
//				theSplat = Instantiate (splat, hit.point + (hit.normal * 2.5), Quaternion.identity);
//				Destroy (theSplat, 2);
//			}
//		}

		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				//var newInstance = Instantiate (Weapon, transform.position, transform.rotation);
				var newInstance = Instantiate (Weapon, Input.GetTouch(i).position, Quaternion.identity);
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			// instantiate an object at the mouse's position, at 20 units away from the camera
			var screenPos = Input.mousePosition;
			screenPos.z = 5;
			var worldPos = GetComponent<Camera>().ScreenToWorldPoint(screenPos);
			var newInstance = Instantiate(Weapon, worldPos, Quaternion.identity);
			//var newInstance = Instantiate (Weapon, transform.position, transform.rotation);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			// instantiate an object in the middle of the screen
			var screenPos = new Vector3(Screen.width/2,Screen.height/2);
			screenPos.z = 5;
			var worldPos = GetComponent<Camera>().ScreenToWorldPoint(screenPos);
			var newInstance = Instantiate(Weapon, worldPos, Quaternion.identity);
		}

		if(Input.GetKeyDown(KeyCode.LeftControl))//take picture
		{

		}

		if(Input.GetKeyDown(KeyCode.Escape))//show weapons menu
		{
			
		}
	}

	void CreateObstacle()
	{
		// Pool
//		if (obstacles[currentPoolIndex] != null)
//		{
//			Destroy(obstacles[currentPoolIndex].gameObject);
//			obstacles[currentPoolIndex] = null;
//		}
//		
//		//obstacles[currentPoolIndex] = Instantiate(Obstacle1) as Transform;
//		
//		Vector3 v3Pos = new Vector3(1.10f, .5f, 10);
//		v3Pos = Camera.main.ViewportToWorldPoint(v3Pos);
//		obstacles[currentPoolIndex] = Instantiate(Obstacle1,v3Pos,Quaternion.identity) as Transform;
//		
//		currentPoolIndex++;
//		
//		if(currentPoolIndex >= NumToGenerateForLevel)
//		{
//			generating = false;
//			CancelInvoke("CreateObstacle");
//		}
//		
//		if (currentPoolIndex >= PoolSize) currentPoolIndex = 0;
	}
}
