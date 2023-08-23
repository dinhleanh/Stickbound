using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Einfügen von Sound in anderen Skripten:
    // FindObjectOfType<AudioManager>().PlaySound("hierDenStringDesAudioClips");


    public Sound[] sounds;

    private static AudioManager instance;


    public ChangeMusicSkript1 changeMusicSkript1;



    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }


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
            s.volumeSafe = s.volume;
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

    private void Update()
    {
        

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

    public void MuteSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.nameOfSoundClip == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " cannot be found!");
            return;
        }

        Debug.Log("Muting sound: " + name);
        s.source.volume = 0f;
    }

    public void UnmuteSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.nameOfSoundClip == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " cannot be found!");
            return;
        }

        Debug.Log("Unmuting sound: " + name);
        s.source.volume = s.volumeSafe;
    }
}
