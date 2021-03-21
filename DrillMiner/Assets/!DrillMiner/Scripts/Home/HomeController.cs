namespace Insepter.DM.Home
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Insepter.DM.Settings;
    public class HomeController : MonoBehaviour
    {
        void Awake() => SettingController.callBackSetClose = () => SettingController.callBackActiveSetting?.Invoke(false);
        public void GameplayScene() => SceneManager.LoadSceneAsync("GameplayScene", LoadSceneMode.Single); //Fade Percent
        public void Setting() => SettingController.callBackActiveSetting?.Invoke(true);
    }
}
