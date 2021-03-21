namespace Insepter.DM.Score
{
    using UnityEngine;
    public class LvUpEvent : MonoBehaviour
    {
        public GameObject lvPanel;
        public void EndLvUp() => lvPanel.SetActive(false);
    }
}