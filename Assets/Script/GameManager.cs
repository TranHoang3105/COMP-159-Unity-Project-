using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayerDied()
    {
        SceneManager.LoadScene("GameOver"); 
    }

    public void BossDefeated()
    {
        SceneManager.LoadScene("VictoryScreen"); 
    }
}
