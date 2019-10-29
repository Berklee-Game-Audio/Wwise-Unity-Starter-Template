using UnityEngine;
using System.Collections;

public class MusicSection2 : MonoBehaviour {

	public GameObject myMusicManager;
	private MusicEngine myMusicEngine;

	// Use this for initialization
	void Start () {
		//find the music manager game object in the scene first
		myMusicManager = GameObject.Find("MusicManager");

		//then get the music engine
		myMusicEngine = myMusicManager.GetComponent<MusicEngine>();
	}

	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter(){
		myMusicEngine.SwitchToMusicB ();

	}
}
