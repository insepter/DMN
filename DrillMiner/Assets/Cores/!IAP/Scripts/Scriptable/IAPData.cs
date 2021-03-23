namespace Insepter.IAP
{
    using UnityEngine;
    using System.Linq;
    using UnityEngine.Purchasing;

    [CreateAssetMenu(fileName = "IAPData", menuName = "IAP/IAPData")]
    public class IAPData : ScriptableObject
    {
        public IAPDetail[] iAPDetails;
        public IAPDetail GetIAPDetail(string id) => iAPDetails.First(item => item.id.Equals(id));

        public IAPDetail SetIAPDetail(string id)
        {
            IAPDetail _iAPDetail = iAPDetails.Where(item => item.id.Equals(id)).First();
            if (_iAPDetail.productType.Equals(ProductType.NonConsumable))
                _iAPDetail.isBought = true;
            else if (_iAPDetail.productType.Equals(ProductType.Consumable))
            {
                if (!_iAPDetail.isBought)
                    _iAPDetail.isBought = ++_iAPDetail.presentCount >= _iAPDetail.maxCount;
            }
            
            if (Currency.CurrencyController.instance)
                Currency.CurrencyController.instance.SetCurrency(_iAPDetail.currencyReward.eCurrency, _iAPDetail.currencyReward.unit);

            return _iAPDetail;
        }
        public IAPDetail SetIAPDetail(IAPDetail iAPDetail)
        {
            if (iAPDetail.productType.Equals(ProductType.NonConsumable))
                iAPDetail.isBought = true;
            else if (iAPDetail.productType.Equals(ProductType.Consumable))
            {
                if (!iAPDetail.isBought)
                    iAPDetail.isBought = ++iAPDetail.presentCount > iAPDetail.maxCount;
            }
            
            if (Currency.CurrencyController.instance)
                Currency.CurrencyController.instance.SetCurrency(iAPDetail.currencyReward.eCurrency, iAPDetail.currencyReward.unit);

            return iAPDetail;
        }
    }
}
