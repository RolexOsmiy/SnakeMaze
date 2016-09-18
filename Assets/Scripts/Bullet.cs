using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
	private int damage = 25;
	private Collider hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision collision) 
	{
		if(collision.transform.tag == "Player" && !isLocalPlayer)
		{
			string uIdentity = collision.transform.name;
			CmdTellServerWhoWasShot(uIdentity, damage);
		}

		else if(collision.transform.tag == "Zombie")
		{
			string uIdentity = collision.transform.name;
			CmdTellServerWhichZombieWasShot(uIdentity, damage);
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
