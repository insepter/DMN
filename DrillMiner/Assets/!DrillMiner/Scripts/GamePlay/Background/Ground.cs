namespace Insepter.DM.GamePlay.EarthFloor
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Ground : MonoBehaviour
    {
        public byte coutItem { get; set; }
        public UnityEngine.UI.RawImage thisRImg;
        public List<SpawnPointItem> allGrid = new List<SpawnPointItem>();

        public void SetSpawnItem()
        {
            List<SpawnPointItem> _all = allGrid.Where(item => item.CheckChild()).ToList();
            if (_all.Count > 0)
            {
                _all[Random.Range(0, _all.Count)].SetItem();
            }
        }
        public void ReturItem()
        {
            allGrid.ForEach(item =>
            {
                if (item.itemGamePlay != null)
                    item.itemGamePlay.ReturnItem();
            });
        }
    }
}
