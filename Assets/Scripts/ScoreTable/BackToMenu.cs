using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void ClickMenu()
    {
        SceneManager.LoadScene( "SampleScene" );
    }
}
