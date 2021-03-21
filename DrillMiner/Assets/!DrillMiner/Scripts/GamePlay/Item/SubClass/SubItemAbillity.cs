namespace Insepter.DM.GamePlay.Items
{
    using Insepter.DM.GamePlay.EarthFloor;
    using System.Collections;
    using UnityEngine;
    public class SubItemAbillity
    {
        readonly float _energy;
        readonly uint _energyRecoverInRandomItem;
        readonly float _ironValue = 10, _bronzeValue = 20, _silverValue = 30, _goldValue = 40, _diamondValue = 10;
        public SubItemAbillity()
        {
            // SetFuelTank //
            _energy = (GamePlayController.instance.maxEnergy * .05f) + GamePlayController.instance.skillData.GetData(EUpgrade.FuelTank).presentLv;
            // SetAtomPython //
            _energyRecoverInRandomItem = GamePlayController.instance.skillData.GetData(EUpgrade.AtomPython).presentLv;
            // SetAllOrePurifier //
            SetOrePurifier(ref _ironValue, EUpgrade.IronOrePurifier);
            SetOrePurifier(ref _bronzeValue, EUpgrade.BronzeOrePurifier);
            SetOrePurifier(ref _silverValue, EUpgrade.SilverOrePurifier);
            SetOrePurifier(ref _goldValue, EUpgrade.GoldOrePurifier);
            SetOrePurifier(ref _diamondValue, EUpgrade.DiamondPurifier);
        }
        void SetOrePurifier(ref float valuePrice, EUpgrade eUpgrade, float rate = .02f) => valuePrice += (valuePrice * GamePlayController.instance.skillData.GetData(eUpgrade).presentLv * rate);
        void HitItemRandomFuel()
        {
            if (_energyRecoverInRandomItem > 0 && Random.value >= .8f)
                GamePlayController.instance.AdjustEnergy(_energyRecoverInRandomItem);
        }
        public void CheckItem(ETypeObject eTypeObject, string nameItem)
        {
            if (eTypeObject.Equals(ETypeObject.Item))
            {
                switch (nameItem)
                {
                    case "IronOre": AddCoin(_ironValue, nameItem); break;
                    case "BronzeOre": AddCoin(_bronzeValue, nameItem); break;
                    case "SilverOre": AddCoin(_silverValue, nameItem); break;
                    case "GoldOre": AddCoin(_goldValue, nameItem); break;
                    case "Diamond": AddDimond(_diamondValue); break;
                    case "Fuel": AddEnergy(_energy); break;
                    case "Booster": BoostSpeed(); break;
                    default: break;
                }
            }
            else
            {
                if (!GamePlayController.instance.isFixedSpeed && !GamePlayController.instance.isImmortal)
                {
                    if (!GamePlayController.instance.isIronCurtain)
                    {
                        switch (nameItem)
                        {
                            case "Ore": HitOre(2); break;
                            case "BigOre": HitOre(3); break;
                            case "Bomb": HitBomb(); break;
                            default: break;
                        }
                        GamePlayController.instance.isNotHitAnyThing = false;
                    }
                    else
                        GamePlayController.instance.GetIronCurtain();
                }
            }
        }
        // Item //
        void AddCoin(float coin, string eItem)
        {
            Debug.Log($"Coin: {coin} -- {eItem}");
            Score.ScoreController.coinRecieve += coin;
            HitItemRandomFuel();
        }
        void AddDimond(float dimond)
        {
            Debug.Log($"Dimond: {dimond}");
            Score.ScoreController.dimondRecieve += dimond;
            HitItemRandomFuel();
        }
        void AddEnergy(float energy) => GamePlayController.instance.AdjustEnergy(energy);

        void BoostSpeed() => GamePlayController.instance.GetTimeFastSpeed();
        // Obstacle //
        void HitOre(float timeFreeze)
        {
            ItemGamePlayController.instance.StartCoroutine(IEHitOre());
            IEnumerator IEHitOre()
            {
                GamePlayController.instance.SetFreezeSpeed();
                yield return new WaitForSeconds(timeFreeze);
                GamePlayController.instance.SetFreezeSpeed(false);
            }
        }
        void HitBomb()
        {
            GamePlayController.instance.AdjustEnergy(-1000);
        }
    }
}
