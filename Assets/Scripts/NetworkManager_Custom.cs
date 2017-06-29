using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager_Custom : NetworkManager {

	public GameObject SelectRace;
	public GameObject Menu;

	int CurrHumans = 0;
	public int HumansMax = 0;

	int CurrAlien = 0;

	public GameObject AlienPref;
	public GameObject HumanPref;

	public void StartupHost()
	{
		SelectRace.SetActive (true);
		Menu.SetActive (false);
	}

	public void JoinGame()
	{
		SetIPAddress();
		SetPort();
		NetworkManager.singleton.StartClient();
	}

	void SetIPAddress()
	{
		string ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	void OnLevelWasLoaded (int level)
	{
		if(level == 0)
		{
            //SetupMenuSceneButtons();
            StartCoroutine(SetupMenuSceneButtons());
		}

		else
		{
			SetupOtherSceneButtons();
		}
	}

	IEnumerator SetupMenuSceneButtons()
	{
        yield return new WaitForSeconds(0.3f);
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

		GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
	}

	void SetupOtherSceneButtons()
	{
		GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
	}

	public void BackMenu()
	{
		SelectRace.SetActive (false);
		Menu.SetActive (true);
	}

	public void Human()
	{
		Debug.Log (CurrHumans);
		if (CurrHumans < HumansMax) 
		{
			NetworkManager.singleton.playerPrefab = HumanPref;
			CurrHumans++;
			SetPort();
			NetworkManager.singleton.StartHost();
		}
	}

	public void Alien()
	{
		Debug.Log (CurrAlien);
		if(CurrAlien < 1)
		{
			NetworkManager.singleton.playerPrefab = AlienPref;
			CurrAlien++;
			SetPort();
			NetworkManager.singleton.StartHost();
		}
	}
}
