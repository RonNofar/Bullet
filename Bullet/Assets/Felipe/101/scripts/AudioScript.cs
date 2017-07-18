using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

    public AudioClip ButtonClickSound;
    public AudioSource ButtonClickSound_Source;
    public float Music_volume;
    public float Master_volume;

    public AudioClip TeamSong;
    public AudioSource TeamSong_Source;

    // Use this for initialization
    void Start () {
        ButtonClickSound_Source.clip = ButtonClickSound;
        TeamSong_Source.clip = TeamSong;
        Music_volume = 0.1f;
        Master_volume= 0.05f;
        TeamSong_Source.Play();
         AudioListener.volume=Master_volume;
    }
	
	// Update is called once per frame
	void Update () {
        AudioListener.volume = Master_volume;
        TeamSong_Source.volume = Music_volume;
        if (Input.GetKeyDown(KeyCode.Space)) {

            ButtonClickSound_Source.Play();
        }
	}
}
