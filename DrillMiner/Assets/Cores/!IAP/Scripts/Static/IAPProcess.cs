namespace Insepter.IAP
{
    using UnityEngine;
    using UnityEngine.Purchasing;
    using System;
    public static class IAPProcess
    {
        public static Action<bool> callBackButtonPurchase { get; set; }
        static Func<IAPData> _callIAPData { get; set; }
        static IAPProcess() => IAPController.callBackCheckPurchase += PurchaseComplete;
        public static void SetIAPData(IAPData iAPData) => _callIAPData = () => iAPData;
        static void PurchaseComplete(PurchaseEventArgs purchaseEvent)
        {
            bool _isButtonActive = _callIAPData.Invoke().SetIAPDetail(purchaseEvent.purchasedProduct.definition.id, purchaseEvent.purchasedProduct.definition.type);
            callBackButtonPurchase?.Invoke(_isButtonActive);
        }
    }
}
