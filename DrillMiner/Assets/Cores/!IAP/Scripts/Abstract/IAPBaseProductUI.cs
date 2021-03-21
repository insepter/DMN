namespace Insepter.IAP
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Purchasing;
    using UnityEngine.UI;

    public abstract class IAPBaseProductUI : MonoBehaviour
    {
        public string id, namepurchaseBtn;
        public Text titleTxt, descriptionTxt, priceTxt;
        public Button purchaseBtn;
        public ProductType productType;
        public EButtonPurchase eButtonPurchase;

        public virtual IAPBaseProductUI SetProduct(Product product, System.Action<string> callSentID)
        {
            id = product.definition.id;
            productType = product.definition.type;

            if (titleTxt)
                titleTxt.text = product.metadata.localizedTitle;
            if (descriptionTxt)
                descriptionTxt.text = product.metadata.localizedDescription;
            if (priceTxt)
                priceTxt.text = namepurchaseBtn = product.metadata.localizedPriceString;

            purchaseBtn.onClick.AddListener(() =>
            {
                switch (eButtonPurchase)
                {
                    case EButtonPurchase.Restore: IAPController.callBackRestore?.Invoke(); break;
                    default:
                        IAPProcess.callBackButtonPurchase = PurchaseComple;
                        callSentID?.Invoke(id);
                        break;
                }
            });
            if (!string.IsNullOrEmpty(namepurchaseBtn))
            {
                Text _purchaseTxt = purchaseBtn.GetComponentInChildren<Text>();
                _purchaseTxt.enabled = true;
                _purchaseTxt.GetComponentInChildren<Text>().text = namepurchaseBtn;
            }

            return this;
        }
        void PurchaseComple(bool isActive)
        {
            Debug.Log($"Complete: {id} -- productType: {productType} -- Active: {isActive}");
            purchaseBtn.interactable = isActive;
        }
    }
}
