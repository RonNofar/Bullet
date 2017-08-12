using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript2 : MonoBehaviour {


    public static AudioScript2 instance;

    //Audio FadeIn and FadeOut
    private static float clipLenght;
    private static bool keepFadingIn;
    private static bool keepFadingOut;

    private static bool keepFadingIn2;
    private static bool keepFadingOut2;
    //private Audio[] ArraySounds;
    public GameObject AudioParent;
    public float MasterVolume;
    public AudioClip[] Sounds;
    private GameObject[] SoundSourceOBJ;
    public int Chosentrack;
    public bool timeToChangeTrack;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timeToChangeTrack = false;

        //for the love of god do not add values above 1 to master volume It will explode your ears
        if (MasterVolume > 1)
        {
            MasterVolume = 1;
        }
        AudioListener.volume = MasterVolume;

        SoundSourceOBJ = new GameObject[Sounds.Length];
        //ArraySounds = new Audio[Sounds.Length];
        for (int i = 0; i < Sounds.Length; i++)
        {
            /*
            ArraySounds[i].createSource();
            ArraySounds[i].setParent(AudioParent);
            ArraySounds[i].setSound(Sounds[i]);
            */

            SoundSourceOBJ[i] = new GameObject("Sound" + i);
            SoundSourceOBJ[i].AddComponent<AudioSource>();
            SoundSourceOBJ[i].transform.parent = AudioParent.transform;
            SoundSourceOBJ[i].GetComponent<AudioSource>().clip = Sounds[i];
            SoundSourceOBJ[i].GetComponent<AudioSource>().volume = 0.2f;

        }
        
        //ArraySounds[Chosentrack].play();
        SoundSourceOBJ[Chosentrack].GetComponent<AudioSource>().loop=true;
        SoundSourceOBJ[0].GetComponent<AudioSource>().loop = true;

        SoundSourceOBJ[0].GetComponent<AudioSource>().volume = 0.03f;
        SoundSourceOBJ[0].GetComponent<AudioSource>().Play();

        AudioScript.FadeInCaller(Chosentrack, 0.005f, SoundSourceOBJ[Chosentrack].GetComponent<AudioSource>().volume, SoundSourceOBJ);

        //AudioScript.FadeOutCaller(0, 0.005f, SoundSourceOBJ);

    }

    //MANAGE
    void Update()
    {

        if (timeToChangeTrack)
        {
            //SoundSourceOBJ[Chosentrack].GetComponent<AudioSource>().volume=0;
            timeToChangeTrack = false;
            SoundSourceOBJ[1].GetComponent<AudioSource>().Play();
            SoundSourceOBJ[1].GetComponent<AudioSource>().volume = 0.5f;
            //AudioScript2.switchMusic(0, 1, 0.005f, SoundSourceOBJ[1].GetComponent<AudioSource>().volume, SoundSourceOBJ);
            //AudioScript2.FadeInCaller2(1, 0.01f, SoundSourceOBJ[1].GetComponent<AudioSource>().volume = 0.5f, SoundSourceOBJ);
            AudioScript2.FadeOutCaller(0, 0.005f, SoundSourceOBJ);
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

    public static void FadeInCaller2(int track, float speed, float maxVolume, GameObject[] trackArray)
    {
        //print("Inside FadeInCaller2 ");
        instance.StartCoroutine(FadeIn2(track, speed, maxVolume, trackArray));
    }
    public static void FadeOutCaller2(int track, float speed, GameObject[] trackArray)
    {
        instance.StartCoroutine(FadeOut2(track, speed, trackArray));
    }

    public static void switchMusic(int trackOut, int trackIn, float speed, float maxVolume, GameObject[] trackArray)
    {
        // AudioScript.FadeOutCaller(trackOut, speed, trackArray);
        //AudioScript.FadeInCaller(trackIn, speed, maxVolume, trackArray);

        AudioScript2.FadeInAndOut(trackOut, trackIn, speed, maxVolume, trackArray);
    }


    //COROUTINES
    static IEnumerator FadeIn(int track, float speed, float maxVolume, GameObject[] trackArray)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        trackArray[track].GetComponent<AudioSource>().volume = 0;
        trackArray[track].GetComponent<AudioSource>().Play();
        float audioVolume = trackArray[track].GetComponent<AudioSource>().volume;
        while (audioVolume < maxVolume && keepFadingIn)
        {
            audioVolume += speed;
            trackArray[track].GetComponent<AudioSource>().volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }
    static IEnumerator FadeOut(int track, float speed, GameObject[] trackArray)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        float audioVolume = trackArray[track].GetComponent<AudioSource>().volume;

        while (audioVolume >= speed && keepFadingOut)
        {
            audioVolume -= speed;
            trackArray[track].GetComponent<AudioSource>().volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
        trackArray[track].GetComponent<AudioSource>().volume = 0;
    }

    static IEnumerator FadeIn2(int track, float speed, float maxVolume, GameObject[] trackArray)
    {
        keepFadingIn2 = true;
        keepFadingOut2 = false;

        trackArray[track].GetComponent<AudioSource>().volume = 0;
        trackArray[track].GetComponent<AudioSource>().Play();
        float audioVolume = trackArray[track].GetComponent<AudioSource>().volume;
        while (audioVolume < maxVolume && keepFadingIn)
        {
            audioVolume += speed;
            trackArray[track].GetComponent<AudioSource>().volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }
    static IEnumerator FadeOut2(int track, float speed, GameObject[] trackArray)
    {
        keepFadingIn2 = false;
        keepFadingOut2 = true;

        float audioVolume = trackArray[track].GetComponent<AudioSource>().volume;

        while (audioVolume >= speed && keepFadingOut)
        {
            audioVolume -= speed;
            trackArray[track].GetComponent<AudioSource>().volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
        trackArray[track].GetComponent<AudioSource>().volume = 0;
    }

    static IEnumerator FadeInAndOut(int trackOut, int trackIn, float speed, float maxVolume, GameObject[] trackArray)
    {
        keepFadingIn2 = true;
        keepFadingOut2 = false;

        keepFadingIn = false;
        keepFadingOut = true;

        float audioVolumeOut = trackArray[trackOut].GetComponent<AudioSource>().volume;

        trackArray[trackIn].GetComponent<AudioSource>().volume = 0;
        trackArray[trackIn].GetComponent<AudioSource>().Play();
        float audioVolumeIn = trackArray[trackIn].GetComponent<AudioSource>().volume;
        while ((audioVolumeIn < maxVolume && keepFadingIn2) || (audioVolumeOut >= speed && keepFadingOut))
        {
            if (audioVolumeIn < maxVolume && keepFadingIn2)
            {
                audioVolumeIn += speed;
                trackArray[trackIn].GetComponent<AudioSource>().volume = audioVolumeIn;
            }
            if (audioVolumeOut >= speed && keepFadingOut)
            {
                audioVolumeOut -= speed;
                trackArray[trackOut].GetComponent<AudioSource>().volume = audioVolumeOut;
            }
            yield return new WaitForSeconds(0.1f);
        }
        trackArray[trackOut].GetComponent<AudioSource>().volume = 0;
    }



    public void BuyClickSound()
    {
        //SoundSourceOBJ[2].GetComponent<AudioSource>().loop=false;
        SoundSourceOBJ[2].GetComponent<AudioSource>().Play();
    }
    public void CloseClickSound()
    {
        //SoundSourceOBJ[2].GetComponent<AudioSource>().loop=false;
        SoundSourceOBJ[3].GetComponent<AudioSource>().Play();
    }
    public void MinusClickSound()
    {
        //SoundSourceOBJ[2].GetComponent<AudioSource>().loop=false;
        SoundSourceOBJ[4].GetComponent<AudioSource>().Play();
    }
    public void PlusClickSound()
    {
        //SoundSourceOBJ[2].GetComponent<AudioSource>().loop=false;
        SoundSourceOBJ[5].GetComponent<AudioSource>().Play();
    }
    public void OpenClickSound()
    {
        //SoundSourceOBJ[2].GetComponent<AudioSource>().loop=false;
        SoundSourceOBJ[6].GetComponent<AudioSource>().Play();
    }


}
