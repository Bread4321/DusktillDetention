using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sounds;
    
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
        for (int i = 0; i < GetComponents<AudioSource>().Length; i++)
        {
            AudioSource comp = GetComponent<AudioSource>();
            Destroy(comp);
            Debug.Log("Removed: " + comp.clip);
        }
    }
}
