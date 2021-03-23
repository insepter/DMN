namespace Insepter.IAP
{
    using Insepter.Currency;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Purchasing;

    public class IAPController : MonoBehaviour, IStoreListener
    {
        public static System.Action<PurchaseEventArgs> callBackCheckPurchase { get; set; }
        public static System.Action callBackRestore { get; private set; }
        [Header("AppleKey")]
        public string productNameAppleSubscription;
        [Header("GooglePlayKey")]
        public string productNameGooglePlaySubscription;
        [Header("Data")]
        public IAPData iAPData;

        public IAPBaseProductUI prefabIAPProduct;
        public Transform spawPointIAPProduct;
        public List<IAPBaseProductUI> iAPBaseProductUIs = new List<IAPBaseProductUI>();
        IStoreController _IstoreController { get; set; }
        IExtensionProvider _IextensionProvider { get; set; }
        void Awake()
        {
            callBackRestore = RestorePurchases;
            IAPProcess.SetIAPData(iAPData);
            if (_IstoreController == null)
                SetFakeStore();
        }
        #region FakeStore
        void SetFakeStore()
        {
            if (IsInitialized())
                return;

            var _builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            System.Array.ForEach(iAPData.iAPDetails, item => _builder.AddProduct(item.id, item.productType, new IDs()
            {
                { productNameAppleSubscription, AppleAppStore.Name },
                { productNameGooglePlaySubscription, GooglePlay.Name }
            }));
            UnityPurchasing.Initialize(this, _builder);
        }
        #endregion

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _IstoreController = controller;
            _IextensionProvider = extensions;

            if (iAPBaseProductUIs.Count <= 0)
            {
                for (int i = 0; i < controller.products.all.Length; i++)
                {
                    IAPBaseProductUI _iAPBaseProductUI = Instantiate(prefabIAPProduct, spawPointIAPProduct).SetProduct(controller.products.all[i], PurchaseButtonClick);
                    _iAPBaseProductUI.SetCountGetReward($"{iAPData.iAPDetails[i].presentCount}/{iAPData.iAPDetails[i].maxCount}");
                    iAPBaseProductUIs.Add(_iAPBaseProductUI);
                }
            }
            else
            {
                for (int i = 0; i < controller.products.all.Length; i++)
                {
                    iAPBaseProductUIs[i].SetProduct(controller.products.all[i], PurchaseButtonClick).SetCountGetReward($"{iAPData.iAPDetails[i].presentCount}/{iAPData.iAPDetails[i].maxCount}");
                }
            }

        }
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            callBackCheckPurchase?.Invoke(purchaseEvent);
            return PurchaseProcessingResult.Complete;
        }
        public void PurchaseButtonClick(string productID)
        {
            if (IsInitialized())
            {
                Product product = _IstoreController.products.WithID(productID);
                if (product != null && product.availableToPurchase)
                    _IstoreController.InitiatePurchase(product);
                else
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
            else
                Debug.Log("BuyProductID FAIL. Not initialized.");
        }
        public void RestorePurchases()
        {
            if (!IsInitialized())
                return;

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
                _IextensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions((result) => Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."));
            else
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
        public void OnInitializeFailed(InitializationFailureReason error) => Debug.Log($"InitializeFailed: {error}");
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => Debug.Log($"PurchaseFailed: {product.definition.id} -- {failureReason}");
        bool IsInitialized() => _IstoreController != null && _IextensionProvider != null;
    }

    #region StructAndClass
    [System.Serializable]
    public class IAPDetail
    {
        public string id;
        public bool isBought;
        public CurrencyDetail currencyReward;
        public ProductType productType;
        [Header("Consumable")]
        public ushort presentCount;
        public ushort maxCount;
    }
    public abstract class PurchaseProgressInstant : MonoBehaviour, IPurchaseResult
    {
        protected virtual void Start() => IAPController.callBackCheckPurchase += PurchaseComplete;
        public abstract void PurchaseComplete(PurchaseEventArgs purchaseEvent);
        protected virtual void OnDisable() => IAPController.callBackCheckPurchase -= PurchaseComplete;
        protected virtual void OnDestroy() => IAPController.callBackCheckPurchase -= PurchaseComplete;
    }
    public abstract class PurchaseProgress : IPurchaseResult
    {
        public PurchaseProgress() => IAPController.callBackCheckPurchase += PurchaseComplete;
        public abstract void PurchaseComplete(PurchaseEventArgs purchaseEvent);
    }
    #endregion

    #region Enum
    public enum EButtonPurchase { Purchase, Restore }
    #endregion

    #region Interface
    public interface IPurchaseResult
    {
        void PurchaseComplete(PurchaseEventArgs purchaseEvent);
    }
    #endregion
}
