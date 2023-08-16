using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip audioClip; // AudioClip to play and spawn
    public string timeInterval = "00:05.000"; // Time interval in mm:ss.msmsms format between creating new instances

    private float nextSpawnTime = 0.0f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        audioSource.Play();

        nextSpawnTime = Time.time + ConvertTimeToSeconds(timeInterval);

        // Destroy the initial AudioSource component after the audio clip length
        Destroy(audioSource, audioClip.length);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime += ConvertTimeToSeconds(timeInterval);
            CreateNewInstance();
        }
    }

    private void CreateNewInstance()
    {
        AudioSource newInstance = gameObject.AddComponent<AudioSource>();
        newInstance.clip = audioClip;
        newInstance.timeSamples = 0; // Start playing from the beginning
        newInstance.Play();

        // Destroy the new instance's AudioSource component after the audio clip length
        Destroy(newInstance, audioClip.length);
    }

    private float ConvertTimeToSeconds(string time)
    {
        string[] timeComponents = time.Split(':');
        int minutes = int.Parse(timeComponents[0]);
        string[] secondsComponents = timeComponents[1].Split('.');
        int seconds = int.Parse(secondsComponents[0]);
        int milliseconds = int.Parse(secondsComponents[1]);

        return minutes * 60.0f + seconds + milliseconds / 1000.0f;
    }
}
