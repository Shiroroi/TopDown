using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Play music when the game starts
        }
    }

    // Function to stop the music
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    // Function to change the music (pass a new AudioClip)
    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource != null && newClip != null)
        {
            audioSource.Stop();  // Stop the current music
            audioSource.clip = newClip;  // Set the new clip
            audioSource.Play();  // Play the new music
        }
    }

    // Function to pause the music
    public void PauseMusic()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }

    // Function to resume the music
    public void ResumeMusic()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
}
