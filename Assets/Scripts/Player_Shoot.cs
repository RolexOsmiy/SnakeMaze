using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Shoot : NetworkBehaviour {

	public int damagePistol = 30;
    public int damageRifle = 40;
	public float range = 200;
	[SerializeField] private Transform camTransform;
	private RaycastHit hit;

    //стрельба
    public float shootCountPistol;
	public float shootTimePistol;

    public float shootCountRifle;
    public float shootTimeRifle;

    public float shootCountGrenade;
    public float shootTimeGrenade;
    //звуки
    AudioSource audio;
	public AudioClip shotPistol;
	public AudioClip reloadPistol;

    public AudioClip shotRifle;
    public AudioClip reloadRifle;
    //патроны
    public int ammoPistol;
	public int curAmmoPistol;

    public int ammoRifle;
    public int curAmmoRifle;
    //перезарядка
    public bool reloadingPistol;
	public float reloadTimePistol = 2f;

    public bool reloadingRifle;
    public float reloadTimeRifle = 3.2f;
    //смена оружия
    public GameObject pistol;
    public bool pistolActive = true;
    public GameObject rifle;
    public bool rifleActive;
    public GameObject grenade;
    public bool grenadeActive;

    // Use this for initialization
    void Awake () 
	{
        audio = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		shootCountPistol -= Time.deltaTime;
        shootCountRifle -= Time.deltaTime;
        shootCountGrenade -= Time.deltaTime;
        CheckIfShooting();

        //смена оружия
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopCoroutine(ReloadPistol());
            StopCoroutine(ReloadRifle());
            pistolActive = true;
            rifleActive = false;
            grenadeActive = false;
            pistol.SetActive(true);
            rifle.SetActive(false);
            grenade.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopCoroutine(ReloadPistol());
            StopCoroutine(ReloadRifle());
            pistolActive = false;
            rifleActive = true;
            grenadeActive = false;
            pistol.SetActive(false);
            rifle.SetActive(true);
            grenade.SetActive(false);
        }        
    }

    void CheckIfShooting()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (pistolActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && shootCountPistol <= 0 && curAmmoPistol >= 0 && reloadingPistol == false)
            {
                ShootPistol();
            }

            if (Input.GetKeyDown(KeyCode.R) && reloadingPistol == false)
            {
                reloadingPistol = true;
                StartCoroutine(ReloadPistol());
                audio.PlayOneShot(reloadPistol);
            }
        }

        if (rifleActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && shootCountRifle <= 0 && curAmmoRifle >= 0 && reloadingRifle == false)
            {
                ShootRifle();
            }

            if (Input.GetKeyDown(KeyCode.R) && reloadingRifle == false)
            {
                reloadingRifle = true;
                StartCoroutine(ReloadRifle());
                audio.PlayOneShot(reloadRifle);
            }
        }
    }

	IEnumerator ReloadPistol() 
	{
		yield return new WaitForSeconds(reloadTimePistol);
		curAmmoPistol = ammoPistol;
		reloadingPistol = false;
    }
    IEnumerator ReloadRifle()
    {
        yield return new WaitForSeconds(reloadTimeRifle);
        curAmmoRifle = ammoRifle;
        reloadingRifle = false;
    }

    void ShootPistol()
	{
		curAmmoPistol -= 1;
		shootCountPistol = shootTimePistol;
		audio.PlayOneShot (shotPistol);
		if(Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, range))
		{
			if(hit.transform.tag == "Player")
			{
				string uIdentity = hit.transform.name;
				CmdTellServerWhoWasShot(uIdentity, damagePistol);
			}

			else if(hit.transform.tag == "Zombie")
			{
				string uIdentity = hit.transform.name;
				CmdTellServerWhichZombieWasShot(uIdentity, damagePistol);
			}
		}
	}

    void ShootRifle()
    {
        curAmmoRifle -= 1;
        shootCountRifle = shootTimeRifle;
        audio.PlayOneShot(shotRifle);
        if (Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, range))
        {
            if (hit.transform.tag == "Player")
            {
                string uIdentity = hit.transform.name;
                CmdTellServerWhoWasShot(uIdentity, damageRifle);
            }

            else if (hit.transform.tag == "Zombie")
            {
                string uIdentity = hit.transform.name;
                CmdTellServerWhichZombieWasShot(uIdentity, damageRifle);
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
