namespace Insepter.DM.Upgrade.Skill
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UpgradeSkillUI : MonoBehaviour,ISetAllUI<(string coin,string dimond)>
    {
        public Text coinTxt;
        public Text dimondTxt;
        public void SetAllUI((string coin, string dimond) item)
        {
            coinTxt.text = item.coin;
            dimondTxt.text = item.dimond;
        }
    }
}
