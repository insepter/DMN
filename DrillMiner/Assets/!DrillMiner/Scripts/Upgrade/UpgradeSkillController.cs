namespace Insepter.DM.Upgrade.Skill
{
    using Common.Image;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Insepter.Currency;
    public class UpgradeSkillController : MonoBehaviour
    {
        public static uint presentSkillrefId { get; set; }
        [Space]
        public UpgradeSkillItem prefabUpgradeSkillItem;
        public Transform spawnPointPrefabUpgradeSkillItem;
        public RectTransform frameSkill;
        public UpgradeSkillData upgradeSkillData;

        public List<UpgradeSkillItem> allSkills = new List<UpgradeSkillItem>();

        ISetAllUI<(string coin, string dimond)> _iSetAllUI;
        void Awake()
        {
            _iSetAllUI = GetComponent<ISetAllUI<(string coin, string dimond)>>();
        }
        void Start()
        {
            for (int i = 0; i < upgradeSkillData.detailSkills.Length; i++)
            {
                UpgradeSkillItem _item = Instantiate(prefabUpgradeSkillItem, spawnPointPrefabUpgradeSkillItem);
                _item.SetAllUI(upgradeSkillData.detailSkills[i]);
                allSkills.Add(_item);
            }
            UpdateUI();
        }
        public void UpdateUI() => _iSetAllUI.SetAllUI((CurrencyController.instance.GetParseNumber(ECurrency.Coin), CurrencyController.instance.GetParseNumber(ECurrency.Dimond)));
        public void UpgradeButton()
        {
            if (upgradeSkillData.detailSkills.Count(item => item.presentLv < 100) > 0)
            {
                DetailSkill[] _allUpgrade = upgradeSkillData.detailSkills.Where(item => item.presentLv < 100).ToArray();

                float _coin = CurrencyController.instance.GetCurrency(ECurrency.Coin);
                float _dimond = CurrencyController.instance.GetCurrency(ECurrency.Dimond);

                float _costCoin = 10000, _costDimond = Mathf.Clamp(100 + (upgradeSkillData.countUpgrade * 5), 100, 350);
                if (_coin >= _costCoin || _dimond >= _costDimond)
                {
                    int _slot = Random.Range(0, _allUpgrade.Length);
                    uint _refId = _allUpgrade[_slot].refId;

                    UpgradeSkillItem _upgradeSkillItem = allSkills.Where(item => item.refId == _refId).First();
                    _upgradeSkillItem.AdjustLv(++_allUpgrade[_slot].presentLv);

                    frameSkill.SetParent(_upgradeSkillItem.transform);
                    frameSkill.SetSizeCenter();

                    if (_coin >= 10000)
                        CurrencyController.instance.SetCurrency(ECurrency.Coin, -_costCoin);
                    else
                        CurrencyController.instance.SetCurrency(ECurrency.Dimond, -_costDimond);

                    upgradeSkillData.countUpgrade++;
                    UpdateUI();
                }
            }
            else
                Debug.Log($"FullAll");
        }
    }
}
