using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public void ClickGame()
    {
        SceneManager.LoadScene( "GameScene" );
    }
}
