using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource
        buttonClick,
        collectItem,
        footStep,
        payment;

    
    public static AudioManager Instance;

    void Awake()
    {
        Instance=this;
    }



    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
