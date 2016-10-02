using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Shoot : NetworkBehaviour {

	private int damage = 25;
	public float range = 200;
	[SerializeField] private Transform camTransform;
	private RaycastHit hit;
	 
	//стрельба
	public bool shoot;
	public float shootCount;
	public float shootTime;
	//звуки
	AudioSource audio;
	public AudioClip shot;
	public AudioClip reload;
	//патроны
	public int ammo;
	public int curAmmo;
	//перезарядка
	public bool reloading;
	public float reloadTime = 3.2f;

	// Use this for initialization
	void Awake () 
	{
		audio = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		shootCount -= Time.deltaTime;
		CheckIfShooting();
	}

	void CheckIfShooting()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		if(Input.GetKey(KeyCode.Mouse0) && shootCount <= 0 && curAmmo >= 0 && reloading == false)
		{
			Shoot();
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			reloading = true;
				StartCoroutine(Reload ());
			audio.PlayOneShot(reload);
		}
	}

	IEnumerator Reload() 
	{
		yield return new WaitForSeconds(reloadTime);
		curAmmo = ammo;
		reloading = false;
	}

	void Shoot()
	{
		curAmmo -= 1;
		shootCount = shootTime;
		audio.PlayOneShot (shot);
		if(Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, range))
		{
			if(hit.transform.tag == "Player")
			{
				string uIdentity = hit.transform.name;
				CmdTellServerWhoWasShot(uIdentity, damage);
			}

			else if(hit.transform.tag == "Zombie")
			{
				string uIdentity = hit.transform.name;
				CmdTellServerWhichZombieWasShot(uIdentity, damage);
			}
		}
	}

	[Command]
	void CmdTellServerWhoWasShot (string uniqueID, int dmg)
	{
		GameObject go = GameObject.Find(uniqueID);
		go.GetComponent<Player_Health>().DeductHealth(dmg);
	}

	[Command]
	void CmdTellServerWhichZombieWasShot (string uniqueID, int dmg)
	{
		GameObject go = GameObject.Find(uniqueID);
		go.GetComponent<Zombie_Health>().DeductHealth(dmg);
	}

}
