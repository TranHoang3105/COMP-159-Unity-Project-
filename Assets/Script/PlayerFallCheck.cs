using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFallCheck : MonoBehaviour
{
    [SerializeField] private float deathY = -35f; // Y value below which player dies

    private void Update()
    {
        if (transform.position.y < deathY)
        {
            Debug.Log("Player fell off!");
            // Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
