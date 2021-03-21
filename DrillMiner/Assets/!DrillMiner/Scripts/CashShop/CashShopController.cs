namespace Insepter.DM.CashShop
{
    using Insepter.Currency;
    using UnityEngine;
    public class CashShopController : MonoBehaviour
    {
        ISetAllUI<(string coin, string dimond)> _iSetAllUI;
        void Awake()
        {
            _iSetAllUI = GetComponent<ISetAllUI<(string coin, string dimond)>>();
        }
        void Start()
        {
            UpdateUI();
        }
        public void UpdateUI() => _iSetAllUI.SetAllUI((CurrencyController.instance.GetParseNumber(ECurrency.Coin), CurrencyController.instance.GetParseNumber(ECurrency.Dimond)));
    }
}
