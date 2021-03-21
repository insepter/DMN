namespace Common.Image
{
    using UnityEngine;
    using UnityEngine.UI;

    public enum EtypyAnchor
    {
        Custome
            , TopLeft, TopCenter, TopRight
            , LeftMiddle, CenterMiddle, RightMiddle
            , BottomLeft, BottomCenter, BottomRight
    }
    public static class ExImage
    {
        public static void SetSize(this Image victim, EtypyAnchor typeAnchor = EtypyAnchor.CenterMiddle, float sizes = 1f, Vector2 anchoMax = default, Vector2 anchoMin = default, Vector2 anchorPosition = default, Vector2 pivot = default)
        {
            #region Internal
            void SetVector(Vector2 anchoMaxs,Vector2 anchoMins, Vector2 pivots)
            {
                anchoMax = anchoMaxs;
                anchoMin = anchoMins;
                pivot = pivots;
            }
            #endregion

            #region TypeAnchor
            switch (typeAnchor)
            {
                case EtypyAnchor.TopLeft : SetVector(new Vector2(0f,1f), new Vector2(0f, 1f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.TopCenter : SetVector(new Vector2(.5f,1f), new Vector2(.5f, 1f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.TopRight : SetVector(new Vector2(1f,1f), new Vector2(1f, 1f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.LeftMiddle : SetVector(new Vector2(0f,.5f), new Vector2(0f, .5f), new Vector2(0f, .5f));
                    break;
                case EtypyAnchor.CenterMiddle : SetVector(new Vector2(.5f,.5f), new Vector2(.5f, .5f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.RightMiddle : SetVector(new Vector2(1f,.5f), new Vector2(1f, .5f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.BottomLeft : SetVector(Vector2.zero, Vector2.zero, Vector2.zero);
                    break;
                case EtypyAnchor.BottomCenter : SetVector(new Vector2(.5f, 0f), new Vector2(.5f, 0f), new Vector2(.5f, 0f));
                    break;
                case EtypyAnchor.BottomRight : SetVector(new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(.5f, .5f));
                    break;
                default : break;
            }
            #endregion

            victim.SetNativeSize();
            victim.rectTransform.sizeDelta = new Vector2(victim.rectTransform.sizeDelta.x / sizes, victim.rectTransform.sizeDelta.y / sizes);
            victim.rectTransform.anchorMax = anchoMax;
            victim.rectTransform.anchorMin = anchoMin;
            victim.rectTransform.anchoredPosition = anchorPosition;
            victim.rectTransform.pivot = pivot;
        }
        public static void SetSize(this RectTransform victim, EtypyAnchor typeAnchor = EtypyAnchor.CenterMiddle, float sizes = 1f, Vector2 anchoMax = default, Vector2 anchoMin = default, Vector2 anchorPosition = default, Vector2 pivot = default)
        {
            #region Internal
            void SetVector(Vector2 anchoMaxs, Vector2 anchoMins, Vector2 pivots)
            {
                anchoMax = anchoMaxs;
                anchoMin = anchoMins;
                pivot = pivots;
            }
            #endregion

            #region TypeAnchor
            switch (typeAnchor)
            {
                case EtypyAnchor.TopLeft:
                    SetVector(new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.TopCenter:
                    SetVector(new Vector2(.5f, 1f), new Vector2(.5f, 1f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.TopRight:
                    SetVector(new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.LeftMiddle:
                    SetVector(new Vector2(0f, .5f), new Vector2(0f, .5f), new Vector2(0f, .5f));
                    break;
                case EtypyAnchor.CenterMiddle:
                    SetVector(new Vector2(.5f, .5f), new Vector2(.5f, .5f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.RightMiddle:
                    SetVector(new Vector2(1f, .5f), new Vector2(1f, .5f), new Vector2(.5f, .5f));
                    break;
                case EtypyAnchor.BottomLeft:
                    SetVector(Vector2.zero, Vector2.zero, Vector2.zero);
                    break;
                case EtypyAnchor.BottomCenter:
                    SetVector(new Vector2(.5f, 0f), new Vector2(.5f, 0f), new Vector2(.5f, 0f));
                    break;
                case EtypyAnchor.BottomRight:
                    SetVector(new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(.5f, .5f));
                    break;
                default: break;
            }
            #endregion

            victim.sizeDelta = new Vector2(victim.sizeDelta.x / sizes, victim.sizeDelta.y / sizes);
            victim.anchorMax = anchoMax;
            victim.anchorMin = anchoMin;
            victim.anchoredPosition = anchorPosition;
            victim.pivot = pivot;
        }
        
        public static void SetSizeCenter(this Image victim, float sizes = 1)
        {
            Vector2 _center = new Vector2(.5f, .5f);
            victim.SetNativeSize();
            victim.rectTransform.sizeDelta = new Vector2(victim.rectTransform.sizeDelta.x / sizes, victim.rectTransform.sizeDelta.y / sizes);
            victim.rectTransform.anchorMax = _center;
            victim.rectTransform.anchorMin = _center;
            victim.rectTransform.pivot = _center;
        }
        public static void SetSizeCenter(this RectTransform victim, float sizes = 1)
        {
            Vector2 _center = new Vector2(.5f, .5f);
            victim.sizeDelta = new Vector2(victim.sizeDelta.x / sizes, victim.sizeDelta.y / sizes);
            victim.anchorMax = _center;
            victim.anchorMin = _center;
            victim.pivot = _center;
            victim.anchoredPosition = Vector2.zero;
        }
    }
}
