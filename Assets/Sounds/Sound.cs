using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource; 
    void Start()
    {
        StartCoroutine(PlaySoundAfterDelay(1.5f)); 
    }

    private IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        audioSource.Play(); 
    }
}
