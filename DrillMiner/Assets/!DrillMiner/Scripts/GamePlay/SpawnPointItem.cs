namespace Insepter.DM.GamePlay.EarthFloor
{
    using Insepter.DM.GamePlay.Items;
    using UnityEngine;
    public class SpawnPointItem : MonoBehaviour
    {
        public ItemGamePlay itemGamePlay { get; set; }
        public bool CheckChild() => (itemGamePlay == null || !itemGamePlay.gameObject.activeSelf);
        public void SetItem()
        {
            itemGamePlay = ItemGamePlayController.instance.GetItem(transform);
            itemGamePlay.transform.SetParent(transform);
            (itemGamePlay.transform as RectTransform).anchoredPosition3D = Vector3.zero;
            (itemGamePlay.transform as RectTransform).localScale = Vector3.one / 2;
            itemGamePlay.gameObject.SetActive(true);
        }
    }
}
