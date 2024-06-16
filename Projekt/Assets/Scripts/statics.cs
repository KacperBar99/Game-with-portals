using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statics : MonoBehaviour
{
    public static bool Finished;
    public static class AudioFadeOut
    {
        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    }
}
