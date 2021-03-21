namespace Insepter.DM.GamePlay
{
    using Common.Singleton;
    using Insepter.DM.GamePlay.EarthFloor;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GamePlayController : ExSingleton<GamePlayController>
    {
        public Action<float> callBackUIGamePlayEnergy { get; set; }
        public Action<string> callBackUIGamePlayScore { get; set; }
        public Func<float> callBackDitance { get; set; }
        // InvestMove //
        public bool isInverst { get; private set; }
        // Speed //
        public float eternalSpeed { get; set; } = 1;
        public float directSpeed { get; set; } = 0;
        // Move //
        public float speedMove { get; private set; } = 100;
        public float defaultSpeed { get; private set; }
        // Energy //
        public float energy { get; private set; } = 100;
        public float maxEnergy { get; private set; }
        // Camera //
        public (float minValue, float maxValue) sizeCamera { get; } = (30, 40);
        // Floor //
        public byte presentLvFloor;
        public Texture2D[] allFloorTexture;

        public Coroutine fastSpeedCrt { get; private set; } = null;
        [Header("Shop")]
        public Shop.ShopItemData shopItemData;
        [Header("Skill")]
        public Upgrade.Skill.UpgradeSkillData skillData;
        public bool isNotHitAnyThing, isIronCurtain;
        [Header("GamePlay")]
        public GameObject checkContinue, prefabHeader, drill;
        public BackgroundMove prefabBackgroundMove;
        public Transform spawPointFloor;
        public GameObject storeBar, enegyAndDepthBar;
        public bool isStartGame, isFixedSpeed, isContinue, isImmortal;
        public List<BackgroundMove> backgroundMoves = new List<BackgroundMove>();
        public override void Awake()
        {
            base.Awake();
            isInverst = PlayerPrefs.GetInt("InvestComtrolSave", 0) == 1;

            // SetOilPurifierMachine //
            if (skillData.GetData(EUpgrade.FuelTank).presentLv > 0)
                energy += skillData.GetData(EUpgrade.FuelTank).presentLv * 3;

            maxEnergy = energy;
            // SetisIronCurtain //
            isIronCurtain = skillData.GetData(EUpgrade.IronCurtain).presentLv > 0;
            // SetSpeed //
            speedMove = shopItemData.GetUse().speed;
            defaultSpeed = speedMove;

            //thisRectCanvas = transform.root.transform as RectTransform;


            BackgroundMove _bgfirstMove = Instantiate(prefabBackgroundMove, spawPointFloor);
            (_bgfirstMove.transform as RectTransform).anchoredPosition = new Vector2(0, -1225);
            _bgfirstMove.floor.SpawnItem();
            backgroundMoves.Add(_bgfirstMove);

            GameObject _header = Instantiate(prefabHeader, _bgfirstMove.transform);
            (_header.transform as RectTransform).anchoredPosition = Vector2.zero;
            (_header.transform as RectTransform).localScale = Vector2.one / 2;


            for (int i = 0; i < 3; i++)
            {
                BackgroundMove _bgMove = Instantiate(prefabBackgroundMove, spawPointFloor);
                _bgMove.gameObject.SetActive(false);
                backgroundMoves.Add(_bgMove);
            }
        }
        void Update()
        {
            if (isStartGame && Input.GetAxis("Horizontal") == 0)
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, sizeCamera.minValue, Time.deltaTime * 5);

            if (isStartGame)
            {
                if (RemainEnergy() > 0)
                {
                    energy = Mathf.Clamp(energy - Time.deltaTime * eternalSpeed, 0, maxEnergy);
                    //Camera.main.orthographicSize = Mathf.Clamp(_sizeCamera.x + (energy / (maxEnergy / (_sizeCamera.y - _sizeCamera.x))), _sizeCamera.x, _sizeCamera.y);

                    callBackUIGamePlayEnergy?.Invoke((energy / maxEnergy));
                    callBackUIGamePlayScore?.Invoke(GetTextureDistantFloor(callBackDitance.Invoke()).ToString("#"));
                }
                else
                {
                    if (isContinue)
                    {
                        isStartGame = false;
                        isContinue = false;
                        checkContinue.SetActive(true);
                    }
                    else
                        EndGame();
                }
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //    isFixedSpeed = !isFixedSpeed;
            //if (Input.GetKeyDown(KeyCode.F))
            //    AdjustEnergy(10);
            //if (Input.GetKeyDown(KeyCode.S))
            //    GameInitCamera(() => { isStartGame = true; GetTimeFastSpeed(); });
        }
        public void StartGame()
        {
            storeBar.SetActive(isStartGame);
            enegyAndDepthBar.SetActive(!isStartGame);
            GameInitCamera(() =>
            {
                isStartGame = true;
                GetTimeFastSpeed();
                drill.SetActive(true);
            });
        }
        public void GetTimeFastSpeed()
        {
            // SetImprovedBoosterDuration //
            float _times = (3 + skillData.GetData(EUpgrade.ImprovedBoosterDuration).presentLv * .25f);

            if (fastSpeedCrt == null)
                fastSpeedCrt = StartCoroutine(IEFastSpeed());

            IEnumerator IEFastSpeed()
            {
                isFixedSpeed = true;
                yield return new WaitWhile(() =>
                {
                    _times -= Time.deltaTime;
                    // SetImprovedBoosterSpeed //
                    eternalSpeed = Mathf.Clamp(3 + (3 * (_times / 4)) + (skillData.GetData(EUpgrade.ImprovedBoosterSpeed).presentLv * .1f), 5, 8);
                    //Debug.Log($"eternalSpeed: {eternalSpeed}");
                    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, sizeCamera.maxValue, Time.deltaTime * 10);
                    return _times > 0;
                });
                isFixedSpeed = false;
                eternalSpeed = 1;
                fastSpeedCrt = null;
            }
        }
        public void GameInitCamera(Action callbackEnd = null)
        {
            StartCoroutine(IEStartGame(3));
            IEnumerator IEStartGame(float times)
            {
                yield return new WaitWhile(() =>
                {
                    times -= Time.deltaTime;
                    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, sizeCamera.minValue, Time.deltaTime * 1);
                    return times > 0;
                });
                callbackEnd?.Invoke();
            }
        }
        public float RemainEnergy() => energy / maxEnergy;
        public float GetSpeed(float ratio = 1) => (speedMove * RemainEnergy() * eternalSpeed) / ratio;
        public void SetFreezeSpeed(bool isFreeze = true) => speedMove = isFreeze ? 0 : defaultSpeed;
        public void AdjustEnergy(float adjustEnergy) => energy = Mathf.Clamp(energy + adjustEnergy, 0, maxEnergy);

        public void ContinuePlay()
        {
            isStartGame = true;
            energy = maxEnergy / 2;
            GetTimeFastSpeed();
            checkContinue.SetActive(false);
        }
        public void EndGame()
        {
            Score.ScoreController.totalDepth = callBackDitance.Invoke();
            SceneManager.LoadScene("ScoreScene");
        }
        public BackgroundMove CheckBackgroundPool()
        {
            if (backgroundMoves.Count(item => !item.gameObject.activeSelf) > 0)
            {
                BackgroundMove _bgMove = backgroundMoves.Where(item => !item.gameObject.activeSelf).First();
                _bgMove.floor.SpawnItem();
                _bgMove.floor.SetTextureFloor();
                _bgMove.gameObject.SetActive(true);
                return _bgMove;
            }
            else
            {
                BackgroundMove _bgMove = Instantiate(prefabBackgroundMove, spawPointFloor);
                _bgMove.floor.SpawnItem();
                _bgMove.floor.SetTextureFloor();
                backgroundMoves.Add(_bgMove);
                return _bgMove;
            }
        }
        public void CheckHideBackground() => backgroundMoves.ForEach(item =>
        {
            if (item != null)
            {
                if (Vector2.Distance(Camera.main.transform.position, item.transform.position) > 1000)
                {
                    item.gameObject.SetActive(false);
                }
            }
        });
        public void GetIronCurtain()
        {
            // SetisIronCurtain //
            float _timeDelay = 60 - (skillData.GetData(EUpgrade.IronCurtain).presentLv * .3f);

            StartCoroutine(IEDelayIronCurtain());
            StartCoroutine(IEImmortal());

            IEnumerator IEDelayIronCurtain()
            {
                isIronCurtain = false;
                yield return new WaitForSeconds(_timeDelay);
                isIronCurtain = true;
            }

            IEnumerator IEImmortal()
            {
                Player.PlayerManage.callBackShield?.Invoke(isImmortal = true);
                yield return new WaitForSeconds(2);
                Player.PlayerManage.callBackShield?.Invoke(isImmortal = false);
            }
        }
        float GetTextureDistantFloor(float dist)
        {
            switch (dist)
            {
                case float _dist when _dist >= 501: SetLv(1); break;
                case float _dist when _dist >= 2001: SetLv(2); break;
                case float _dist when _dist >= 4001: SetLv(3); break;
                case float _dist when _dist >= 5001: SetLv(4); break;
                case float _dist when _dist >= 7001: SetLv(5); break;
                case float _dist when _dist >= 9001: SetLv(6); break;
                case float _dist when _dist >= 10001: SetLv(7); break;
                case float _dist when _dist >= 12001: SetLv(8); break;
                case float _dist when _dist >= 14001: SetLv(9); break;
                case float _dist when _dist >= 16001: SetLv(10); break;
            }
            return dist;
            void SetLv(byte lv) => presentLvFloor = lv;
        }
    }
}
