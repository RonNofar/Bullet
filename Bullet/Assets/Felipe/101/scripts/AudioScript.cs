using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

    public GameObject Menu;

    public AudioClip ButtonClickSound;
    public AudioSource ButtonClickSound_Source;
    public float Music_volume;
    public float Master_volume;

    public AudioClip TeamSong;
    public AudioClip KardashevMenuSong;
    public AudioSource TeamSong_Source;
    public AudioSource KardashevSource;
    public bool fadeTime;

    // Use this for initialization
    void Start () {
        fadeTime = false;
        ButtonClickSound_Source.clip = ButtonClickSound;
        TeamSong_Source.clip = TeamSong;
        Music_volume = 0.1f;
        Master_volume= 0.2f;
        TeamSong_Source.Play();
         AudioListener.volume=Master_volume;
    }
	
	// Update is called once per frame
	void Update () {
        if (Menu.GetComponent<MenuController>().audioFadeTime)
        {
            fadeTime = true;
        }
        if (fadeTime)
        {
            fadeTime = false;
            Menu.GetComponent<MenuController>().audioFadeTime = false;
            //CancelInvoke();
            StartCoroutine(AudioFadeOut.FadeOut(TeamSong_Source, 70f));
        }
        if (Menu.GetComponent<MenuController>().kardashevMenuMusicTime)
        {
            Menu.GetComponent<MenuController>().kardashevMenuMusicTime = false;
            TeamSong_Source.Stop();
            KardashevSource.volume = Master_volume;
            KardashevSource.clip = KardashevMenuSong;
            KardashevSource.Play();

        }

      //  AudioListener.volume = Master_volume;
       // TeamSong_Source.volume = Music_volume;
        if (Input.GetKeyDown(KeyCode.Space)) {

            ButtonClickSound_Source.Play();
        }
	}
    public static class AudioFadeOut
    {

        public static IEnumerator FadeOut( AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    }
}
