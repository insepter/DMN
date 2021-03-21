namespace Insepter.Currency
{
    using UnityEngine;
    using System.Linq;
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "Currency/CurrencyData")]
    public class CurrencyData : ScriptableObject
    {
        public CurrencyDetail[] currencyDetails;
        public CurrencyDetail GetCurrencyDetail(ECurrency eCurrency) => currencyDetails.Where(item=> item.eCurrency.Equals(eCurrency)).First();
    }
}
