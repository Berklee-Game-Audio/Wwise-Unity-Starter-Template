using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicEngine : MonoBehaviour {

	public AudioClip musicA;
	public float musicAFadeInTime;
	public float musicAFadeOutTime;

	public AudioClip musicB;
	public float musicBFadeInTime;
	public float musicBFadeOutTime;

	public AudioClip transitionAToB;
	public float nextSectionStartTimeAfterAtoBTransitionStart;
	public AudioClip transitionBToA;
	public float nextSectionStartTimeAfterBtoATransitionStart;

	public AudioSource musicAAudioSource;
	public AudioSource musicBAudioSource;

	public AudioSource transitionAudioSource;

	public AudioMixerSnapshot musicBusAOn;
	public AudioMixerSnapshot musicBusAOff;
	public AudioMixerSnapshot musicBusBOn;
	public AudioMixerSnapshot musicBusBOff;

	private float timeElapsed = 0.0f;
	private float nextPlayTime = 0.0f;
	private bool playingTransition = false;
	private bool musicBusA = true;

	public bool useIntroduction = false;
	public AudioClip introductionAudioClip;
	public float introductionOverlapTime = 0.0f;



	// Use this for initialization
	void Awake () {
		Debug.Log ("Music engine is awake");
		
		if(useIntroduction && introductionAudioClip != null){
			//hooray we have an introduction
			transitionAudioSource.clip = introductionAudioClip;
			musicBusAOff.TransitionTo (0.0f);
			musicBusBOff.TransitionTo (0.0f);

		} else { 
			musicAAudioSource.clip = musicA;
			musicBAudioSource.clip = musicB;
			musicBusAOff.TransitionTo (0.0f);
			musicBusBOff.TransitionTo (0.0f);
		} 
			

	}

	void Start () {
		Debug.Log ("Music engine has started");

		if(useIntroduction && introductionAudioClip != null){
			Debug.Log("Playing introduction.");
			transitionAudioSource.Play();
			nextPlayTime = introductionAudioClip.length - introductionOverlapTime;
			musicBusA = false;
			playingTransition = true;
			//musicBAudioSource.Play ();
			//musicBusBOn.TransitionTo (0.0f);
		} else {
			Debug.Log("No introduction.");
			playingTransition = false;
			musicAAudioSource.Play ();
			//musicBAudioSource.Play ();
			musicBusAOn.TransitionTo (musicAFadeInTime);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}

	// 
	void FixedUpdate () {
		timeElapsed += Time.deltaTime;
		if(playingTransition){
			if (timeElapsed > nextPlayTime) {
				//play the next piece of music
				if (musicBusA) {
					FinishSwitchingToB ();
					musicBusA = false;
					musicAAudioSource.Stop ();
					musicBusAOff.TransitionTo (0.0f);
				} else {
					FinishSwitchingToA ();
					musicBusA = true;
					musicBAudioSource.Stop ();
					musicBusBOff.TransitionTo (0.0f);
				}
					
				playingTransition = false;
			}
		}
	}

	void OnDestroy(){
		musicBusAOff.TransitionTo (0.0f);
		musicBusBOff.TransitionTo (0.0f);
		Debug.Log ("The music engine object has been destroyed.");
	}


	public void SwitchToMusicB(){
		if (!musicBusA) {
			return;
		}

		Debug.Log ("SwitchToMusicB");
		timeElapsed = 0.0f;
		nextPlayTime = nextSectionStartTimeAfterAtoBTransitionStart;

		//begin playing the transition
		transitionAudioSource.clip = transitionAToB;
		transitionAudioSource.Play ();
		playingTransition = true;

		//fade out music bus A
		musicBusAOff.TransitionTo (musicAFadeOutTime);

		//musicBusBOn.TransitionTo (0.0f);

	}

	void FinishSwitchingToB() {
		Debug.Log ("Finish switching to B");
		musicBAudioSource.clip = musicB;
		musicBAudioSource.Play ();
		musicBusBOn.TransitionTo (musicBFadeInTime);

	}

	public void SwitchToMusicA(){
		if (musicBusA) {
			return;
		}
		Debug.Log ("SwitchToMusicA");
		timeElapsed = 0.0f;
		nextPlayTime = nextSectionStartTimeAfterBtoATransitionStart;

		//begin playing the transition
		transitionAudioSource.clip = transitionBToA;
		transitionAudioSource.Play ();
		playingTransition = true;

		//fade out music bus A
		musicBusBOff.TransitionTo (musicBFadeOutTime);

		//musicBusBOn.TransitionTo (0.0f);

	}

	void FinishSwitchingToA() {
		Debug.Log ("Finish switching to A");
		musicAAudioSource.clip = musicA;
		musicAAudioSource.Play ();
		musicBusAOn.TransitionTo (musicAFadeInTime);

	}




}
