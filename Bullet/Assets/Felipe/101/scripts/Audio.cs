using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {


    private float Volume=0.05f;
    private AudioClip Sound;
    private GameObject SoundSource;
    private GameObject AudioParent;

    public Audio(GameObject Parent, AudioClip Sound)
    {
        this.AudioParent = Parent;
        this.Sound = Sound;

        this.SoundSource = new GameObject("Sound");
        this.SoundSource.AddComponent<AudioSource>();
        this.SoundSource.transform.parent = AudioParent.transform;
        this.SoundSource.GetComponent<AudioSource>().clip = Sound;

    }
    public Audio()
    {
        this.SoundSource = new GameObject("Sound");
        this.SoundSource.AddComponent<AudioSource>();

    }
    public void setParent(GameObject NewParent)
    {
        this.AudioParent = NewParent;
        this.SoundSource.transform.parent = AudioParent.transform;
    }
    public void setVolume(float NewVolume)
    {
        this.Volume = NewVolume;
    }
    public void setSound(AudioClip NewSound)
    {
        this.Sound = NewSound;
        this.SoundSource.GetComponent<AudioSource>().clip = Sound;
    }
    public void setSource(GameObject NewSource)
    {
        this.SoundSource = NewSource;
        if (this.SoundSource.AddComponent<AudioSource>() != null) {
            this.SoundSource.AddComponent<AudioSource>();
        }
    }

    public void createSource()
    {
        this.SoundSource = new GameObject("Sound");
        this.SoundSource.AddComponent<AudioSource>();
    }

    public GameObject getParent()
    {
        return this.AudioParent;

    }
    public float getVolume(float NewVolume)
    {
        return this.Volume;
    }
    public AudioClip getSound()
    {
        return this.Sound;
    }
    public GameObject getSource()
    {
        return this.SoundSource;
    }

    public void play()
    {
        this.SoundSource.GetComponent<AudioSource>().Play();
    }

}
