namespace Insepter.DM.CashShop
{
    using UnityEngine;
    using UnityEngine.UI;
    public class CashShopUI : MonoBehaviour, ISetAllUI<(string coin, string dimond)>
    {
        public Text coinTxt, dimondTxt;
        public void SetAllUI((string coin, string dimond) item)
        {
            coinTxt.text = item.coin;
            dimondTxt.text = item.dimond;
        }
    }
}
