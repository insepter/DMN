namespace Insepter.Ads
{
    using System.Linq;
    using UnityEngine;
    [CreateAssetMenu(fileName = "AdsData", menuName = "Ads/AdsData")]
    public class AdsData : ScriptableObject
    {
        public AdsDetail[] adsDetails;
        public AdsDetail GetAdsReward(string id) => adsDetails.First(item => item.id.Equals(id));
        public AdsDetail SetAdsDetail(string id)
        {
            AdsDetail _adsDetai = adsDetails.Where(item => item.id.Equals(id)).First();
            if (_adsDetai.eAdsReward.Equals(EAdsReward.NonConsumable))
                _adsDetai.isGotten = true;
            else if (_adsDetai.eAdsReward.Equals(EAdsReward.Consumable))
            {
                if (!_adsDetai.isGotten)
                    _adsDetai.isGotten = ++_adsDetai.presentCount >= _adsDetai.maxCount;
            }

            if (Currency.CurrencyController.instance)
                Currency.CurrencyController.instance.SetCurrency(_adsDetai.currencyReward.eCurrency, _adsDetai.currencyReward.unit);

            return _adsDetai;
        }
        public AdsDetail SetAdsDetail(AdsDetail adsDetail, EAdsReward eAdsReward)
        {
            if (eAdsReward.Equals(EAdsReward.NonConsumable))
                adsDetail.isGotten = true;
            else if (eAdsReward.Equals(EAdsReward.Consumable))
            {
                if (!adsDetail.isGotten)
                    adsDetail.isGotten = ++adsDetail.presentCount > adsDetail.maxCount;
            }

            if (Currency.CurrencyController.instance)
                Currency.CurrencyController.instance.SetCurrency(adsDetail.currencyReward.eCurrency, adsDetail.currencyReward.unit);

            return adsDetail;
        }
    }
}
