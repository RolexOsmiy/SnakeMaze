using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public GameObject bullet;
	public Transform spawnBullet;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.Mouse0))
			{
			Instantiate (bullet, spawnBullet.position, spawnBullet.rotation);
			}
		
	}
}
