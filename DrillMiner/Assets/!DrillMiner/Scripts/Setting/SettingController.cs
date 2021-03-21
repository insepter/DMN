namespace Insepter.DM.Settings
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class SettingController : MonoBehaviour
    {
        [Header("UI")]
        public Toggle soundTog;
        public Toggle invertTog;

        public static System.Action<bool> callBackActiveSetting { get; private set; }
        public static System.Action callBackSetClose { get; set; }

        public ButtonLink buttonLink;
        Canvas _thisCanvas;
        void Awake()
        {
            _thisCanvas = GetComponent<Canvas>();

            invertTog.isOn = PlayerPrefs.GetInt("InvestComtrolSave", 0) == 1;
            soundTog.isOn = PlayerPrefs.GetInt("SoundSave", 1) == 1;

            invertTog.onValueChanged.AddListener(result => PlayerPrefs.SetInt("InvestComtrolSave", result ? 1 : 0));
            soundTog.onValueChanged.AddListener(result => PlayerPrefs.SetInt("SoundSave", result ? 1 : 0));
        }
        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            callBackActiveSetting = result => _thisCanvas.enabled = result;
            buttonLink.callBackButton = callBackSetClose;
        }
        public void SettingCanvas(bool isAvtive) => callBackActiveSetting?.Invoke(isAvtive);
    }
}
