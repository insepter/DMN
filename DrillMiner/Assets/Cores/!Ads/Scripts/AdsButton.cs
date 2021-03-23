namespace Insepter.Ads
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Advertisements;

    [RequireComponent(typeof(Button))]
    public class AdsButton : MonoBehaviour
    {
        public string mySurfacingId;
        public EAdsReward eAdsReward;
        public System.Action<AdsDetail> callBackFinish = null;
        Button _thisBtn;
        void Awake()
        {
            _thisBtn = GetComponent<Button>();
            AdsController.adsButtons.Add(this);
        }
        void Start() => _thisBtn.onClick.AddListener(() => 
        { 
            Advertisement.Show(mySurfacingId);
            AdsController.presentEAdsReward = eAdsReward;
            AdsController.callBackPresentAdsBtn = callBackFinish;
        });
        public void SetButtonReady(bool isActive) => _thisBtn.interactable = isActive;
    }
}