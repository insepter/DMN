namespace Insepter.Ads
{
    using UnityEngine;
    using UnityEngine.UI;
    public abstract class AdsBaseUI : MonoBehaviour
    {
        public Text countGetRewardTxt;
        public AdsButton adsButton;
        void Awake()
        {
            if (adsButton)
                adsButton.callBackFinish = AdsFinish;
        }

        public void SetCountGetReward(string result)
        {
            if (countGetRewardTxt)
                countGetRewardTxt.text = result;
        }
        void AdsFinish(AdsDetail adsDetail)
        {
            if (adsDetail.isGotten)
                adsButton.SetButtonReady(!adsDetail.isGotten);

            SetCountGetReward($"{adsDetail.presentCount}/{adsDetail.maxCount}");
        }
    }
}
