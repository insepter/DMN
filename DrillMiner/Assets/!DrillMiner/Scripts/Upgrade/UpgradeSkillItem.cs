namespace Insepter.DM.Upgrade.Skill
{
    using UnityEngine;
    using UnityEngine.UI;
    public class UpgradeSkillItem : MonoBehaviour, ISetAllUI<DetailSkill>
    {
        public RawImage skillIconRImg;
        public Text lvTxt;
        public uint refId;
        public void SetAllUI(DetailSkill item)
        {
            refId = item.refId;
            lvTxt.text = $"Lv. {item.presentLv}";
            skillIconRImg.texture = item.icon;
        }
        public void AdjustLv(uint lv) => lvTxt.text = $"Lv. {lv}";

    }
}