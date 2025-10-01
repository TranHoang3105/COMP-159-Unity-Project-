using UnityEngine;
using UnityEngine.SceneManagement;

public class TurorialBackController : MonoBehaviour
{

public void OnBackClick()
{
    SceneManager.LoadScene("NewMenu"); 
}

}
