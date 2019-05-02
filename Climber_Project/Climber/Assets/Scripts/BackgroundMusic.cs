using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource backgroundMusic;
    private void Start()
    { 
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.volume = 0;
        if (!backgroundMusic.isPlaying)
            StartCoroutine(PlayBackgroundMusic());
        DontDestroyOnLoad(backgroundMusic);
    }

    IEnumerator PlayBackgroundMusic()
    {
        yield return new WaitForSeconds(.3f);
        backgroundMusic.Play();
        while (backgroundMusic.volume < 0.55f)
        {
            backgroundMusic.volume += 0.05f;
            yield return new WaitForSeconds(.1f);
        }
    }

}
