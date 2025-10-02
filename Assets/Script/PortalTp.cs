using UnityEngine;
using Unity.Cinemachine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform targetPortal;  
    [SerializeField] private CinemachineCamera vcam;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Player entered portal: " + gameObject.name);

        // Teleport player
        collision.transform.position = targetPortal.position;

       
    }
}
