using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {
	public GameObject[] Guns;
	public GameObject TakeText;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = new Ray(transform.position,transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 2.5f)) 
		{
			{
				if (hit.collider.tag == "Gun") 
				{
					TakeText.SetActive (true);
				} 
				else 
				{
					TakeText.SetActive (false);
				}
			}
		}
		if(Input.GetKeyUp(KeyCode.Alpha1))
		{
			Guns[0].SetActive (true);
		}
		if(Input.GetKeyUp(KeyCode.Alpha2))
		{
			Guns[1].SetActive (true);
		}
		if(Input.GetKeyUp(KeyCode.Alpha3))
		{
			Guns[2].SetActive (true);
		}
		if(Input.GetKeyUp(KeyCode.Alpha4))
		{
			Guns[3].SetActive (true);
		}
		if(Input.GetKeyUp(KeyCode.Alpha5))
		{
			Guns[4].SetActive (true);
		}
		if(Input.GetKeyUp(KeyCode.Alpha6))
		{
			Guns[5].SetActive (true);
		}
	}

}
