using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip clique;
    public AudioClip arremesso;
    public AudioClip acerto;
    public AudioClip erro;
    public AudioClip faseCompleta;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
