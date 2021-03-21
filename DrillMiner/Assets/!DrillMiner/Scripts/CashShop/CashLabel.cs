namespace Insepter.DM.CashShop
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CashLabel : MonoBehaviour
    {
        public Text countLimiteTxt;
        public CashButton cashButton;

        void Start()
        {
            if (!cashButton.isUnlimite)
                SetCountLimite();
        }
        public void SetCountLimite() => countLimiteTxt.text = $"{cashButton.countLimite.x}/{cashButton.countLimite.y}";
    }
}
