namespace Insepter.DM.Shop
{
    using UnityEngine;
    using UnityEngine.UI;
    public class ShopUI : MonoBehaviour, ISetAllUI<string>
    {
        public Text coinRemain;
        public Button buyOrUseBtn;
        Text _buyOrUseTxt;
        void Awake() => _buyOrUseTxt = buyOrUseBtn.GetComponentInChildren<Text>();
        public void SetAllUI(string item) => coinRemain.text = item;
        public void SetBuyText(string result) => _buyOrUseTxt.text = result;
    }
}
