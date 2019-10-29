using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class MusicTestLoops : MonoBehaviour {

    private bool m_Fire1;
    public float jumpToPercentage = 0.90f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_Fire1 = CrossPlatformInputManager.GetButtonDown("Fire1");

        if (m_Fire1)
        {
            //Debug.Log("Fire1 button pressed");
            MoveAudioPlaybackPosition();
            m_Fire1 = false;
        }


    }

    void MoveAudioPlaybackPosition()
    {
        //first find all audiosources on this object
        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach(AudioSource audioSourceItem in audioSources)
        {
            if(audioSourceItem.isPlaying)
            {
                int length = audioSourceItem.clip.samples;
                audioSourceItem.timeSamples = Mathf.RoundToInt(length * jumpToPercentage);

            }

        }

    }


}
