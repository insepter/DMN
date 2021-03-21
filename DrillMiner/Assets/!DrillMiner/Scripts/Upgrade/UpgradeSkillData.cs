namespace Insepter.DM.Upgrade.Skill
{
    using UnityEngine;
    using System.Linq;
    [CreateAssetMenu(fileName = "SkillData", menuName = "Upgrade/SkillData")]
    public class UpgradeSkillData : ScriptableObject
    {
        public uint countUpgrade;
        public DetailSkill[] detailSkills;

        [ContextMenu("ResetLv")]
        void ResetLv() => System.Array.ForEach(detailSkills, item => item.presentLv = 0);
        [ContextMenu("SetMax")]
        void SetMAxLv() => System.Array.ForEach(detailSkills, item => item.presentLv = 99);
        public DetailSkill GetData(EUpgrade eUpgrade) => detailSkills.Where(item => item.eUpgrade.Equals(eUpgrade)).First();
    }
}
