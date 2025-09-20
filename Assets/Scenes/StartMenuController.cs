using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
public void OnStartClick()
{
    SceneManager.LoadScene("1st Level"); 
}

public void OnExitClick()
{
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    Application.Quit();
}

}
