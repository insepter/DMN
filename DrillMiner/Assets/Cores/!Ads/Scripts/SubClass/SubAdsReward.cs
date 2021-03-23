namespace Insepter.Ads
{
    using UnityEngine;
    using UnityEngine.Advertisements;

    public class SubAdsReward
    {
        public void GetReward(string surfacingId, ShowResult showResult, System.Func<AdsData> callAdsData)
        {
            switch (showResult)
            {
                case ShowResult.Finished:
                    Debug.Log($"Get Reward");
                    GetReward();
                    break;
                case ShowResult.Skipped:
                    Debug.Log($"Skip Reward");
                    GetReward();
                    break;
                case ShowResult.Failed: Debug.LogWarning("The ad did not finish due to an error."); break;
                default: break;
            }
            void GetReward()
            {
                AdsDetail _adsDetai = callAdsData.Invoke().SetAdsDetail(surfacingId);
                Debug.Log($"_currencyDetail: {_adsDetai.currencyReward.eCurrency} --- {_adsDetai.currencyReward.unit} --- {AdsController.presentEAdsReward}");
                AdsController.callBackPresentAdsBtn?.Invoke(_adsDetai);
            }
        }
    }
}
