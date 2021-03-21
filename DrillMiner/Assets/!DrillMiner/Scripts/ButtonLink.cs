namespace Insepter.DM
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    public class ButtonLink : MonoBehaviour
    {
        public string sceneName;
        public System.Action callBackButton { get; set; }
        void Start() => GetComponent<Button>().onClick.AddListener(() =>
        {
            if (callBackButton == null)
                SceneManager.LoadScene(sceneName);
            else
                callBackButton?.Invoke();
        });
    }
}
