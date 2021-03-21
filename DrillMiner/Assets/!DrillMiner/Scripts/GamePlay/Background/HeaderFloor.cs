namespace Insepter.DM.GamePlay.EarthFloor
{
    using UnityEngine;
    using UnityEngine.UI;

    public class HeaderFloor : MonoBehaviour
    {
        public Texture headerTxr;
        public byte countSpawn;
        [ContextMenu("SetHead")]
        void SetHeader()
        {
            (transform as RectTransform).anchoredPosition = new Vector2(-(transform as RectTransform).rect.height, 0);
            RawImage _rawImg = new GameObject().AddComponent<RawImage>();
            for (int i = 0; i < countSpawn; i++)
            {
                RawImage _raw = Instantiate(_rawImg, transform);
                _raw.texture = headerTxr;
                _raw.SetNativeSize();
            }
            Destroy(_rawImg.gameObject);
        }
        void OnDisable() => Destroy(gameObject);
    }
}
