using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick( PointerEventData eventData )
    {
        if( eventData.pointerCurrentRaycast.gameObject.name == gameObject.name )
        {
            SceneManager.LoadScene( "SampleScene" );
        }
    }
}
