namespace Insepter.Currency
{
    using System.Linq;
    using UnityEngine;
    using Common.Singleton;
    using System.Text;

    public class CurrencyController : ExSingleton<CurrencyController>, ICurrency<ECurrency>
    {
        [SerializeField] CurrencyData _currencyChange;
        public float GetCurrency(ECurrency typeCurrency) => _currencyChange.currencyDetails.Where(item => item.eCurrency.Equals(typeCurrency)).First().unit;
        public float GetCurrency(CurrencyDetail currencyDetail) => _currencyChange.currencyDetails.Where(item => item.eCurrency.Equals(currencyDetail.eCurrency)).First().unit;
        public void SetCurrency(ECurrency typeCurrency, float coin) => _currencyChange.currencyDetails.Where(item => item.eCurrency.Equals(typeCurrency)).First().unit += coin;
        public void SetCurrency(CurrencyDetail currencyDetail) => _currencyChange.currencyDetails.Where(item => item.eCurrency.Equals(currencyDetail.eCurrency)).First().unit += currencyDetail.unit;
        public string GetParseNumber(ECurrency typeCurrency)
        {
            float _currencyRemain = GetCurrency(typeCurrency);
            switch (Mathf.Log10(GetCurrency(typeCurrency)))
            {
                case float _c when _c >= 6: return SetNumber(6, "M");
                case float _c when _c >= 3: return SetNumber(3, "K");
                default: return _currencyRemain.ToString("N0");
            }
            string SetNumber(int count, string formats)
            {
                StringBuilder _number = new StringBuilder().Append(_currencyRemain).Remove(((int)Mathf.Log10(_currencyRemain) - (count - 1)), count);
                return $"{_number}{formats}";
            }
        }
    }

    #region StructAndClass
    [System.Serializable]
    public class CurrencyDetail
    {
        public ECurrency eCurrency;
        public float unit;

        public static CurrencyDetail operator +(CurrencyDetail currencyStore, CurrencyDetail adjustCurrency) => new CurrencyDetail()
        {
            eCurrency = currencyStore.eCurrency,
            unit = currencyStore.unit + adjustCurrency.unit
        };
        public static CurrencyDetail operator -(CurrencyDetail currencyStore, CurrencyDetail adjustCurrency) => new CurrencyDetail()
        {
            eCurrency = currencyStore.eCurrency,
            unit = currencyStore.unit - adjustCurrency.unit
        };
    }
    #endregion

    #region Enum
    public enum ECurrency { Coin, Dimond } // Can Adjust Type
    #endregion

    #region Interface
    public interface ICurrency<T>
    {
        float GetCurrency(T typeCurrency);
        float GetCurrency(CurrencyDetail currencyDetail);
        void SetCurrency(T typeCurrency, float coin);
        void SetCurrency(CurrencyDetail currencyDetail);
    }
    #endregion
}
