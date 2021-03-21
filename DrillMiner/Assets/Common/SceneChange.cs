namespace Common.Scene
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;
    [RequireComponent(typeof(ICanvasRaycastFilter))]
    public class SceneChange : MonoBehaviour, IPointerClickHandler
    {
        public string sceneName;
        public void OnPointerClick(PointerEventData eventData) => SceneManager.LoadScene(sceneName);
    }
}
