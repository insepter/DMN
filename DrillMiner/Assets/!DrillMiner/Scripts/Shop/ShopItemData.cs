namespace Insepter.DM.Shop
{
    using UnityEngine;
    using System.Linq;
    [CreateAssetMenu(fileName = "ShopItemData", menuName = "Shop/ShopItemData")]
    public class ShopItemData : ScriptableObject
    {
        public DetailShopItem[] detailShopItems;

        public float GetPrice(uint refId) => detailShopItems.Where(item => item.refId == refId).First().cost;

        public void SetUnlock(uint refId) => detailShopItems.Where(item => item.refId == refId).First().isUnlock = true;
        public bool GetUnlock(uint refId) => detailShopItems.Where(item => item.refId == refId).First().isUnlock;

        public void SetUse(uint refId)
        {
            System.Array.ForEach(detailShopItems, item => item.isUse = false);
            detailShopItems.Where(item => item.refId == refId).First().isUse = true;
        }
        public Texture2D GetUseTexture() => (detailShopItems.Count(item => item.isUse) > 0) ? detailShopItems.Where(item => item.isUse).First().icon : detailShopItems.First().icon;
        public DetailShopItem GetUse() => (detailShopItems.Count(item => item.isUse) > 0) ? detailShopItems.Where(item => item.isUse).First() : detailShopItems.First();

        [ContextMenu("ResetUsuAndUnlock")]
        void ResetAll()
        {
            System.Array.ForEach(detailShopItems, item =>
            {
                item.isUnlock = false;
                item.isUse = false;
            });
        }
    }
}
