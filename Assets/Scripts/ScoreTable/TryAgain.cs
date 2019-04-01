using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick( PointerEventData eventData )
    {
        if( eventData.pointerCurrentRaycast.gameObject.name == gameObject.name )
        {
            SceneManager.LoadScene( "GameScene" );
        }
    }
}
