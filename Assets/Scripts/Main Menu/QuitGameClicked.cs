using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGameClicked : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == gameObject.name)
        {
            Application.Quit();
        }
    }
}
