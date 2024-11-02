using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
    /*
     * --------------------- SOUNDS LIST -----------------------
     */    
    public AudioSource buttonClick;
    public AudioSource sucessSound;

    //----------------------------------------------------------
    

    private bool muteOn = false;

	static public SoundManager instance;    

    float musicFadeOutSpeed;
	float musicFadeOutMinVolume;
    

	void Start()
	{
		instance = this;
        muteOn = false;        
    }


	public void Play(AudioSource source)
	{		
        source.Play();		
	}


	public void Mute(bool on)
	{
		muteOn = on;
		if (on) {
			AudioListener.volume = 0;
		} else {
			AudioListener.volume = 1;
		}		
	}


	public void ToogleMute()
	{
		Mute (!IsMuted());
	}


	public bool IsMuted()
	{
		return muteOn;
	}


    public void PlayButtonClickSound()
    {
        buttonClick.Play();
    }
	
}



