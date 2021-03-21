namespace Insepter.DM.GamePlay.EarthFloor
{
    using UnityEngine;
    public class BackgroundMove : MonoBehaviour
    {
        public byte presentLvFloor;
        public BackgroundMove leftBg, rightBg, downBg;
        public GameObject leftCol, rightCol, downCol;
        [HideInInspector] public Floor floor;
        void Awake()
        {
            floor = GetComponentInChildren<Floor>();
        }
        void OnEnable() => transform.SetAsLastSibling();
        void Start() => SetTextureFloor();
        void OnDisable()
        {
            floor.ReturnItem();
            SetTextureFloor();
        }
        public void SetDefault()
        {
            leftBg = null;
            rightBg = null;
            downBg = null;

            leftCol.SetActive(true);
            rightCol.SetActive(true);
            downCol.SetActive(true);
        }
        public void HideCol()
        {
            SetHide(this);
            SetHide(leftBg);
            SetHide(rightBg);
            SetHide(downBg);

            void SetHide(BackgroundMove background)
            {
                if (background != null)
                {
                    background.downCol.SetActive(false);
                    background.leftCol.SetActive(false);
                    background.rightCol.SetActive(false);
                }
            }
        }
        void SetTextureFloor()
        {
            if (GamePlayController.instance.presentLvFloor != presentLvFloor)
            {
                presentLvFloor = GamePlayController.instance.presentLvFloor;
                floor.SetTextureFloor();
            }
        }
    }
}
