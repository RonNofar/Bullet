using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

    public static AudioScript instance;

    //Audio FadeIn and FadeOut
    private static float clipLenght;
    //private Audio[] ArraySounds;
    public GameObject AudioParent;
    public float MasterVolume;
    public AudioClip[] Sounds;
    private GameObject[] SoundSourceOBJ;
    public int Chosentrack;
    public bool timeToChangeTrack;
    public bool timeToChangeTrack2;
    void Awake()
    {
        instance = this;
    }
    void Start ()
    {
        timeToChangeTrack = false;
        timeToChangeTrack2 = false;

        //for the love of god do not add values above 1 to master volume It will explode your ears
        if (MasterVolume > 1) {
            MasterVolume = 1;
        }
        AudioListener.volume = MasterVolume;

        SoundSourceOBJ = new GameObject[Sounds.Length];
        //ArraySounds = new Audio[Sounds.Length];
        for (int i =0; i< Sounds.Length; i++)
        {
            /*
            ArraySounds[i].createSource();
            ArraySounds[i].setParent(AudioParent);
            ArraySounds[i].setSound(Sounds[i]);
            */

            SoundSourceOBJ[i]=new GameObject("Sound"+i);
            SoundSourceOBJ[i].AddComponent<AudioSource>();
            SoundSourceOBJ[i].transform.parent = AudioParent.transform;
            SoundSourceOBJ[i].GetComponent<AudioSource>().clip = Sounds[i];
            SoundSourceOBJ[i].GetComponent<AudioSource>().volume = 0.2f;

        }

        //ArraySounds[Chosentrack].play();
        SoundSourceOBJ[Chosentrack].GetComponent<AudioSource>().Play();
        AudioScript.FadeInCaller(0, 0.05f, SoundSourceOBJ[Chosentrack].GetComponent<AudioSource>().volume, SoundSourceOBJ);
        //AudioScript.FadeOutCaller(0, 0.005f, SoundSourceOBJ);

    }

    //MANAGE
    void Update()
    {
        
        if (timeToChangeTrack)
        {
            //SoundSourceOBJ[Chosentrack].GetComponent<AudioSource>().volume=0;
            timeToChangeTrack = false;
              //SoundSourceOBJ[1].GetComponent<AudioSource>().Play();
              //SoundSourceOBJ[1].GetComponent<AudioSource>().volume = 0.5f;
            //AudioScript.switchMusic(0, 1, 0.005f, SoundSourceOBJ[1].GetComponent<AudioSource>().volume, SoundSourceOBJ);
            AudioScript.FadeInCaller(1, 0.01f, 0.5f, SoundSourceOBJ);
            AudioScript.FadeOutCaller(0, 0.005f, SoundSourceOBJ);
        }
    }



    //CALLERS
    public static void FadeInCaller(int track, float speed, float maxVolume, GameObject[] trackArray)
    {
        instance.StartCoroutine(FadeIn(track, speed, maxVolume, trackArray));
    }
    public static void FadeOutCaller(int track, float speed, GameObject[] trackArray)
    {
        //print("Inside FadeOutCaller ");
        instance.StartCoroutine(FadeOut(track, speed, trackArray));
    }


    //COROUTINES
    static IEnumerator FadeIn (int track,float speed,float maxVolume, GameObject[] trackArray)
    {
        trackArray[track].GetComponent<AudioSource>().volume = 0;
        trackArray[track].GetComponent<AudioSource>().Play();
        float audioVolume = trackArray[track].GetComponent<AudioSource>().volume;
        while(audioVolume < maxVolume ){
            audioVolume += speed;
            trackArray[track].GetComponent<AudioSource>().volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }
    static IEnumerator FadeOut (int track, float speed, GameObject[] trackArray)
    {
        float audioVolume = trackArray[track].GetComponent<AudioSource>().volume;

        while (audioVolume >= speed)
        {
            audioVolume -= speed;
            trackArray[track].GetComponent<AudioSource>().volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
        trackArray[track].GetComponent<AudioSource>().volume = 0;
    }

    public void ClickSound()
    {
        //SoundSourceOBJ[2].GetComponent<AudioSource>().loop=false;
        SoundSourceOBJ[2].GetComponent<AudioSource>().Play();
    }
}

