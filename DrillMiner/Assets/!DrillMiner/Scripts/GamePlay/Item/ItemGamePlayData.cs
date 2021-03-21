namespace Insepter.DM.GamePlay.Items
{
    using System.Collections.Generic;
    using UnityEngine;
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item/ItemData")]
    public class ItemGamePlayData : ScriptableObject
    {
        public List<DetailObject<EItem>> detailItemObjects;
        public List<DetailObject<EObstacle>> detailObstacleObjects;
    }
}
