using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartGameClicked : MonoBehaviour, IPointerClickHandler
{
    public string GameSceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == gameObject.name)
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
