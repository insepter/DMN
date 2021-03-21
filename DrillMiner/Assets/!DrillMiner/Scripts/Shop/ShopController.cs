namespace Insepter.DM.Shop
{
    using UnityEngine;
    using Common.Singleton;
    using Common.Image;
    using Insepter.Currency;
    using System.Collections.Generic;
    using System.Linq;

    public class ShopController : ExSingleton<ShopController>
    {
        public ShopItemData shopItemData;
        public ShopItemUI prefabShopItem;
        public Transform spawnPointPrefabShopItem;
        public RectTransform frameItem;
        public bool isUnlock;
        public List<ShopItemUI> shopItemUIs = new List<ShopItemUI>();
        uint? _refIdItem;
        ShopUI _shopUI;
        public override void Awake()
        {
            base.Awake();
            _shopUI = GetComponent<ShopUI>();
        }
        void Start()
        {
            SetUI();
            System.Array.ForEach(shopItemData.detailShopItems, item =>
            {
                ShopItemUI _item = Instantiate(prefabShopItem, spawnPointPrefabShopItem);
                _item.SetAllUI((item.icon, item.isUse ? "Use" : item.isUnlock ? "Unlock" : item.cost.ToString(), item.refId));
                if (item.isUse)
                {
                    frameItem.SetParent(_item.transform);
                    frameItem.SetSizeCenter();
                }
                shopItemUIs.Add(_item);
            });

            if (shopItemData.detailShopItems.Any(item => item.isUse))
            {
                for (int i = 0; i < shopItemData.detailShopItems.Length; i++)
                {
                    if (shopItemData.detailShopItems[i].isUse)
                    {
                        _refIdItem = shopItemData.detailShopItems[i].refId;
                        SetBuy();
                    }
                }
            }
        }
        void SetUI() => _shopUI.SetAllUI(CurrencyController.instance.GetParseNumber(ECurrency.Coin));
        public void SetFrame(uint refId, Transform parent)
        {
            _refIdItem = refId;
            frameItem.SetParent(parent);
            frameItem.SetSizeCenter();

            SetBuy();
        }
        public void BuyItem()
        {
            if (!isUnlock)
            {
                if (_refIdItem != null)
                {
                    float _coin = CurrencyController.instance.GetCurrency(ECurrency.Coin);
                    CurrencyDetail _price = new CurrencyDetail() { unit = shopItemData.GetPrice(_refIdItem.GetValueOrDefault()) };

                    if (_coin >= _price.unit)
                    {
                        CurrencyController.instance.SetCurrency(ECurrency.Coin, -_price.unit);
                        shopItemData.SetUnlock(_refIdItem.GetValueOrDefault());
                        SetUI();
                        SetBuy();
                        SetItemUI(false);
                    }
                }
            }
            else
            {
                shopItemData.SetUse(_refIdItem.GetValueOrDefault());
                SetItemUI();
            }
        }
        void SetBuy()
        {
            isUnlock = shopItemData.GetUnlock(_refIdItem.GetValueOrDefault());
            _shopUI.SetBuyText(isUnlock ? "USE" : "BUY");

            Debug.Log($"isUnlock: {isUnlock} -- {_refIdItem.GetValueOrDefault()}");
        }
        void SetItemUI(bool isSetFrame = true)
        {
            for (int i = 0; i < shopItemData.detailShopItems.Length; i++)
            {
                DetailShopItem _detailShopItem = shopItemData.detailShopItems[i];
                shopItemUIs[i].SetAllUI((_detailShopItem.icon, _detailShopItem.isUse ? "Use" : _detailShopItem.isUnlock ? "Unlock" : _detailShopItem.cost.ToString(), _detailShopItem.refId));
                if (_detailShopItem.isUse && isSetFrame)
                {
                    frameItem.SetParent(shopItemUIs[i].transform);
                    frameItem.SetSizeCenter();
                }
            }
        }
    }
}
