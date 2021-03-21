namespace Insepter.DM.GamePlay.Items
{
    using UnityEngine;
    using UnityEngine.UI;
    public class ItemGamePlay : MonoBehaviour, ITypeItem
    {
        public RawImage iconRImg;
        public string nameItem { get; set; }
        public ETypeObject eTypeObject { get; set; }
        public void SentItem()
        {
            ItemGamePlayController.instance.subItemAbillity.CheckItem(eTypeObject, nameItem);
            ItemGamePlayController.instance.CheckItemRemain(nameItem);
            ReturnItem();
        }
        public void ReturnItem()
        {
            if (ItemGamePlayController.instance)
                transform.SetParent(ItemGamePlayController.instance.transform);

            gameObject.SetActive(false);
        }
        public void SetItem<T>(T nameItemed, ETypeObject eTypeObjected, Texture2D icon)
        {
            nameItem = nameItemed.ToString();
            eTypeObject = eTypeObjected;

            iconRImg.texture = icon;
            iconRImg.SetNativeSize();

            GetComponent<BoxCollider2D>().size = new Vector2(iconRImg.rectTransform.rect.width, iconRImg.rectTransform.rect.height);
        }
        void OnEnable() => CancelInvoke("SetDestroy");
        void OnDisable() => Invoke("SetDestroy", 5);
        void SetDestroy()
        {
            ItemGamePlayController.instance.allItems.Remove(this);
            Destroy(gameObject);
        }
    }
}
