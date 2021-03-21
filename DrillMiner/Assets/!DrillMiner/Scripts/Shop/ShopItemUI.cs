namespace Insepter.DM.Shop
{
    using UnityEngine;
    using UnityEngine.UI;
    public class ShopItemUI : MonoBehaviour, ISetAllUI<(Texture2D icon, string price, uint refIds)>
    {
        public RawImage iconRImag;
        public Text priceTxt;
        public uint refId;

        Button _thisBtn;
        void Awake() => _thisBtn = GetComponent<Button>();
        void Start() => _thisBtn.onClick.AddListener(() => ShopController.instance.SetFrame(refId, transform));
        public void SetAllUI((Texture2D icon, string price, uint refIds) item)
        {
            iconRImag.texture = item.icon;
            priceTxt.text = item.price;
            refId = item.refIds;
        }
    }
}
