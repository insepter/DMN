namespace Insepter.DM.CashShop
{
    using UnityEngine;
    public class CashButton : MonoBehaviour
    {
        public bool isUnlimite;
        public Vector2 countLimite = new Vector2();

        public void GetResult()
        {
            if (isUnlimite)
            {
                Debug.Log($"Reward");
            }
            else if (countLimite.x < countLimite.y)
            {
                countLimite.x++;
                Debug.Log($"Reward: {countLimite}");
            }
        }
    }
}
