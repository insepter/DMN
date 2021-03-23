namespace Insepter.IAP
{
    using UnityEngine;
    using UnityEngine.Purchasing;
    using System;
    public static class IAPProcess
    {
        public static Action<IAPDetail> callBackButtonPurchase { get; set; }
        static Func<IAPData> _callIAPData { get; set; }
        static IAPProcess() => IAPController.callBackCheckPurchase += PurchaseComplete;
        public static void SetIAPData(IAPData iAPData) => _callIAPData = () => iAPData;
        static void PurchaseComplete(PurchaseEventArgs purchaseEvent)
        {
            IAPDetail _iapDetail = _callIAPData.Invoke().SetIAPDetail(purchaseEvent.purchasedProduct.definition.id);
            callBackButtonPurchase?.Invoke(_iapDetail);
        }
    }
}
