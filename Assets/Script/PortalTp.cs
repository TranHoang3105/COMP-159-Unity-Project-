using UnityEngine;
using Unity.Cinemachine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform targetPortal;  
    [SerializeField] private CinemachineCamera vcam;  

    [Header("Audio Settings")]
    [SerializeField] private AudioSource bgmSource;   // The AudioSource playing music
    [SerializeField] private AudioClip newMusic;      // The new music background

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Player entered portal: " + gameObject.name);

        // Teleport player
        collision.transform.position = targetPortal.position;

        // Change background music
        if (bgmSource != null && newMusic != null)
        {
            if (bgmSource.clip != newMusic) // only switch if it's not already playing
            {
                bgmSource.clip = newMusic;
                bgmSource.Play();
            }
        }
    }
}
