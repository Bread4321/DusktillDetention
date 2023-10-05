using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sounds = new AudioClip[11];

    public void Play(string name, bool loop)
    {
        foreach (AudioClip clip in sounds)
        {
            if (string.Equals(clip.name, name))
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = clip;
                source.loop = loop;
                source.Play();

                Debug.Log("Playing: " + source.clip + " Looping: " + source.loop);
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            Debug.Log("Removing: " + source.clip);
            Destroy(source);
        }
    }
}
