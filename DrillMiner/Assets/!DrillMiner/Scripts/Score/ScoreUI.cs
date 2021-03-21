namespace Insepter.DM.Score
{
    using UnityEngine;
    using UnityEngine.UI;
    public class ScoreUI : MonoBehaviour, ISetAllUI<(string coin, string depth, string exp, string lv)>
    {
        public Text coinTxt, depthTxt, expTxt, lvTxt;
        public Slider expSld;
        public void SetAllUI((string coin, string depth, string exp, string lv) item)
        {
            coinTxt.text = item.coin;
            depthTxt.text = item.depth;
            expTxt.text = item.exp;
            lvTxt.text = item.lv;
        }
        public void SetExpValue(float exp) => expSld.value = exp;
        public void SetCoin(string coin) => coinTxt.text = coin;
        public void SetDepth(string depth) => depthTxt.text = depth;
    }
}
