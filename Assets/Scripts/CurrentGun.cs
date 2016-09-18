using UnityEngine;
using System.Collections;

public class CurrentGun : MonoBehaviour {

	public GameObject Weapon01;
	public GameObject Weapon02;
	public GameObject Weapon03;
	public GameObject Weapon04;
	public GameObject Weapon05;
	public GameObject Weapon06;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.Alpha1))
			{
				Weapon01.SetActive(true);
				Weapon02.SetActive(false);
				Weapon03.SetActive(false);
				Weapon04.SetActive(false);
				Weapon05.SetActive(false);
				Weapon06.SetActive(false);
			}
		if (Input.GetKey(KeyCode.Alpha2))
				{
					Weapon01.SetActive(false);
					Weapon02.SetActive(true);
					Weapon03.SetActive(false);
					Weapon04.SetActive(false);
					Weapon05.SetActive(false);
					Weapon06.SetActive(false);
				}
		if (Input.GetKey(KeyCode.Alpha3))
					{
						Weapon01.SetActive(false);
						Weapon02.SetActive(false);
						Weapon03.SetActive(true);
						Weapon04.SetActive(false);
						Weapon05.SetActive(false);
						Weapon06.SetActive(false);
					}
		if (Input.GetKey(KeyCode.Alpha4))
						{
							Weapon01.SetActive(false);
							Weapon02.SetActive(false);
							Weapon03.SetActive(false);
							Weapon04.SetActive(true);
							Weapon05.SetActive(false);
							Weapon06.SetActive(false);
						}
		if (Input.GetKey(KeyCode.Alpha5))
							{
								Weapon01.SetActive(false);
								Weapon02.SetActive(false);
								Weapon03.SetActive(false);
								Weapon04.SetActive(false);
								Weapon05.SetActive(true);
								Weapon06.SetActive(false);
							}
		if (Input.GetKey(KeyCode.Alpha6))
								{
									Weapon01.SetActive(false);
									Weapon02.SetActive(false);
									Weapon03.SetActive(false);
									Weapon04.SetActive(false);
									Weapon05.SetActive(false);
									Weapon06.SetActive(true);
								}
	}
}
