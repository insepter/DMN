namespace Insepter.DM.GamePlay.EarthFloor
{
    using UnityEngine;
    using Common.Number;
    using System.Collections.Generic;
    using System.Linq;

    public class Floor : MonoBehaviour
    {
        public Ground[] grounds;
        public void SpawnItem()
        {
            List<int> _all = ExGetNumbers.GetNumbers(grounds.Length).ToList();
            for (int i = 0; i < Items.ItemGamePlayController.instance.maxItemPerFloor; i++)
            {
                if (_all.Count <= 0)
                    _all = ExGetNumbers.GetNumbers(grounds.Length).ToList();

                int _ran = Random.Range(0, _all.Count);
                grounds[_ran].SetSpawnItem();
                _all.RemoveAt(_ran);
            }

            Items.ItemGamePlayController.instance.CheckItemSpawn();
        }
        public void ReturnItem()
        {
            for (int i = 0; i < grounds.Length; i++)
            {
                grounds[i].ReturItem();
            }
        }
        public void SetTextureFloor() => System.Array.ForEach(grounds, item => item.thisRImg.texture = GamePlayController.instance.allFloorTexture[GamePlayController.instance.presentLvFloor]);
    }
}
