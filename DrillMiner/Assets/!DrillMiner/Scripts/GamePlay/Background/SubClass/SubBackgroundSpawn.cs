namespace Insepter.DM.GamePlay.EarthFloor
{
    using Insepter.DM.GamePlay.Cameras;
    using UnityEngine;
    public class SubBackgroundSpawn
    {
        public void SpawnBackground(string nameMat, RectTransform target)
        {
            switch (nameMat)
            {
                case "Down": SetDown(target); break;
                case "Left": SetLeft(target); break;
                default: SetRight(target); break;
            }
            GamePlayController.instance.CheckHideBackground();
        }
        void RefBg(ref BackgroundMove target, BackgroundMove refBgMove) => target = refBgMove;
        void RefActive(ref GameObject target, bool isActive = false) => target.SetActive(isActive);
        void SetDown(RectTransform target)
        {
            // Down //
            BackgroundMove _moveDown = GamePlayController.instance.CheckBackgroundPool();
            RectTransform _basePos = SetRect(_moveDown.transform as RectTransform, new Vector2(target.anchoredPosition.x, -target.rect.height + target.anchoredPosition.y - 835));
            // DownLeft //
            BackgroundMove _moveDownLeft = GamePlayController.instance.CheckBackgroundPool();
            SetRect(_moveDownLeft.transform as RectTransform, new Vector2(_basePos.anchoredPosition.x - _basePos.rect.width - 380, -target.rect.height + target.anchoredPosition.y - 835));
            // DownRight //
            BackgroundMove _moveDownRight = GamePlayController.instance.CheckBackgroundPool();
            SetRect(_moveDownRight.transform as RectTransform, new Vector2(_basePos.anchoredPosition.x + _basePos.rect.width + 380, -target.rect.height + target.anchoredPosition.y - 835));

            _moveDown.SetDefault();
            _moveDownLeft.SetDefault();
            _moveDownRight.SetDefault();

            RefBg(ref _moveDown.leftBg, _moveDownLeft);
            RefBg(ref _moveDown.rightBg, _moveDownRight);

            RefBg(ref _moveDownRight.leftBg, _moveDown);
            RefBg(ref _moveDownLeft.rightBg, _moveDown);

            RefActive(ref _moveDown.leftCol);
            RefActive(ref _moveDown.rightCol);

            RefActive(ref _moveDownRight.leftCol);
            RefActive(ref _moveDownLeft.rightCol);
        }
        void SetLeft(RectTransform target)
        {
            // Down //
            BackgroundMove _moveDown = GamePlayController.instance.CheckBackgroundPool();
            RectTransform _basePos = SetRect(_moveDown.transform as RectTransform, new Vector2(target.anchoredPosition.x, -target.rect.height + target.anchoredPosition.y - 835));
            // DownLeft //
            BackgroundMove _moveDownLeft = GamePlayController.instance.CheckBackgroundPool();
            SetRect(_moveDownLeft.transform as RectTransform, new Vector2(_basePos.anchoredPosition.x - _basePos.rect.width - 380, _basePos.anchoredPosition.y));
            // Left //
            BackgroundMove _moveLeft = GamePlayController.instance.CheckBackgroundPool();
            SetRect(_moveLeft.transform as RectTransform, new Vector2(target.anchoredPosition.x - target.rect.width - 380, target.anchoredPosition.y));

            _moveDown.SetDefault();
            _moveDownLeft.SetDefault();
            _moveLeft.SetDefault();

            RefBg(ref _moveDown.leftBg, _moveDownLeft);
            RefBg(ref _moveDownLeft.rightBg, _moveDown);
            RefBg(ref _moveLeft.downBg, _moveDownLeft);

            RefActive(ref _moveDown.leftCol);
            RefActive(ref _moveDownLeft.rightCol);

            RefActive(ref _moveLeft.rightCol);
            RefActive(ref _moveLeft.leftCol);
            RefActive(ref _moveLeft.downCol);
        }
        void SetRight(RectTransform target)
        {
            // Down //
            BackgroundMove _moveDown = GamePlayController.instance.CheckBackgroundPool();
            RectTransform _basePos = SetRect(_moveDown.transform as RectTransform, new Vector2(target.anchoredPosition.x, -target.rect.height + target.anchoredPosition.y - 835));
            // DownRight //
            BackgroundMove _moveDownRight = GamePlayController.instance.CheckBackgroundPool();
            SetRect(_moveDownRight.transform as RectTransform, new Vector2(_basePos.anchoredPosition.x + _basePos.rect.width + 380, _basePos.anchoredPosition.y));
            // Right //
            BackgroundMove _moveRight = GamePlayController.instance.CheckBackgroundPool();
            SetRect(_moveRight.transform as RectTransform, new Vector2(target.anchoredPosition.x + target.rect.width + 380, target.anchoredPosition.y));

            _moveDown.SetDefault();
            _moveDownRight.SetDefault();
            _moveRight.SetDefault();

            RefBg(ref _moveDown.rightBg, _moveDownRight);
            RefBg(ref _moveDownRight.leftBg, _moveDown);
            RefBg(ref _moveRight.downBg, _moveDownRight);

            RefActive(ref _moveDown.rightCol);
            RefActive(ref _moveDownRight.leftCol);

            RefActive(ref _moveRight.rightCol);
            RefActive(ref _moveRight.leftCol);
            RefActive(ref _moveRight.downCol);
        }
        RectTransform SetRect(RectTransform target, Vector2 pos)
        {
            target.anchoredPosition = pos;
            return target;
        }
    }
}