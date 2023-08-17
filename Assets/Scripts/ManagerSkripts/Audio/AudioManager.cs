using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Einfügen von Sound in anderen Skripten:
    // FindObjectOfType<AudioManager>().Play("hierDenStringDesAudioClips");


    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach ( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        PlaySound("Theme");
    }


    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.nameOfSoundClip == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " cannot be found!");
            return;
        }
        s.source.Play();
    }
}
