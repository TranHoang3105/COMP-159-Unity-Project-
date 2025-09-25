using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void OnRestartClick()
{
    SceneManager.LoadScene("1st Level");         
}

    public void OnMenuClick()
{
    SceneManager.LoadScene("Main Menu"); 
}

}
