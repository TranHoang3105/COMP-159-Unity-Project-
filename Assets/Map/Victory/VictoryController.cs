using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{

    public void OnMenuClick()
{
    SceneManager.LoadScene("NewMenu"); 
}

}
