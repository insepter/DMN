namespace Insepter.Ads
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Advertisements;

    public class AdsController : MonoBehaviour, IUnityAdsListener
    {
        public static List<AdsButton> adsButtons { get; set; } = new List<AdsButton>();
        public static System.Action<AdsDetail> callBackPresentAdsBtn { get; set; }
        public static EAdsReward presentEAdsReward { get; set; }

        public AdsData adsData;
        public string googlePlayID = "1234567";
        public string iosID = "1234567";
        public bool testMode = true;

        readonly SubAdsReward _subAdsReward = new SubAdsReward();
        void Awake() => Advertisement.AddListener(this);
        void Start()
        {
#if UNITY_IOS
            Advertisement.Initialize(iosID, testMode);
#elif UNITY_ANDROID
            Advertisement.Initialize(googlePlayID, testMode);
#endif
        }
        public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult) => _subAdsReward.GetReward(surfacingId, showResult, () => adsData);
        public void OnUnityAdsReady(string surfacingId)
        {
            if (adsButtons.Count > 0)
            {
                AdsButton _adsBtn = null;
                adsButtons.ForEach(item =>
                {
                    item.SetButtonReady(false);
                    if (item.mySurfacingId.Equals(surfacingId))
                    {
                        Debug.Log($"Active: {surfacingId}");
                        item.SetButtonReady(Advertisement.IsReady(surfacingId));
                        _adsBtn = item;
                        return;
                    }
                });
                if (_adsBtn != null)
                    adsButtons.Remove(_adsBtn);
            }
        }
        public void OnUnityAdsDidError(string message) => Debug.Log($"Error: {message}");
        public void OnUnityAdsDidStart(string surfacingId) => Debug.Log($"Start: {surfacingId}");
        public void OnDestroy() => Advertisement.RemoveListener(this);
    }

#region StructAndClass
    [System.Serializable]
    public class AdsDetail
    {
        public string id;
        public bool isGotten;
        public Currency.CurrencyDetail currencyReward;
        public EAdsReward eAdsReward;
        [Header("Consumable")]
        public ushort presentCount;
        public ushort maxCount;
    }
#endregion

#region Enum
    public enum EAdsReward { Consumable, NonConsumable, Subscription }
#endregion
}
