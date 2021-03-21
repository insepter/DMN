namespace Insepter.DM.GamePlay.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIGamePlayManage : MonoBehaviour,ISetAllUI<(float remainEney,string score)>
    {
        public Image energyBarImg;
        public Text scoreTxt;
        void Start()
        {
            GamePlayController.instance.callBackUIGamePlayEnergy = SetEnergy;
            GamePlayController.instance.callBackUIGamePlayScore = SetScore;
        }
        public void SetAllUI((float remainEney, string score) item)
        {
            energyBarImg.fillAmount = item.remainEney;
            scoreTxt.text = item.score;
        }
        public void SetEnergy(float fill) => energyBarImg.fillAmount = fill;
        public void SetScore(string score) => scoreTxt.text = score;

    }
}
