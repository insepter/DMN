namespace Insepter.DM.GamePlay.Player
{
    using UnityEngine;
    public class PointDistanceScore : MonoBehaviour
    {
        void Start() => GamePlayController.instance.callBackDitance = () => (transform as RectTransform).anchoredPosition.y;
        void Update()
        {
            if (GamePlayController.instance.RemainEnergy() > 0 && GamePlayController.instance.isStartGame)
                transform.position += Vector3.up * Time.deltaTime * GamePlayController.instance.GetSpeed(100);
        }
    }
}
