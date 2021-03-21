namespace Insepter.DM.GamePlay.Items
{
    using Common.Singleton;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    public class ItemGamePlayController : ExSingleton<ItemGamePlayController>
    {
        [Serializable]
        public class ItemTable
        {
            public string nameObject;
            public ETypeObject eTypeObject;
            public ushort count;
            [Range(0, 100)] public byte percentRate;
        }
        public float maxItemPerFloor { get => 50; }
        public ItemGamePlayData ItemGamePlayData;// { get; private set; }
        public ItemGamePlay prefabsItem;

        public ushort totalItem;
        [Range(0, 10)] public byte itemSpawnPerOnceObstacle;
        public List<ItemTable> itemTables = new List<ItemTable>();
        public List<ItemGamePlay> allItems = new List<ItemGamePlay>();

        public SubItemAbillity subItemAbillity;
        //[ContextMenu("Set")]
        //void Set()
        //{
        //    for (int i = 0; i < Enum.GetValues(typeof(EItem)).Length; i++)
        //    {
        //        itemTables.Add(new ItemTable() { nameObject = Enum.GetNames(typeof(EItem))[i] });
        //    }
        //    for (int i = 0; i < Enum.GetValues(typeof(EObstacle)).Length; i++)
        //    {
        //        itemTables.Add(new ItemTable() { nameObject = Enum.GetNames(typeof(EObstacle))[i] });
        //    }
        //}
        void Start()
        {
            subItemAbillity = new SubItemAbillity();
        }
        public ItemGamePlay GetItem(Transform spawnPoint)
        {
            ItemGamePlay _item = null;

            if (allItems.Count(item => !item.gameObject.activeSelf) > 0)
                _item = allItems.Where(item => !item.gameObject.activeSelf).First();
            else
                allItems.Add(_item = Instantiate(prefabsItem, spawnPoint));

            return SetItem(_item);
        }
        ItemGamePlay SetItem(ItemGamePlay item)
        {
            totalItem++;
            int _slot = CheckRateSpawn();

            if ((totalItem > 0 && totalItem % itemSpawnPerOnceObstacle == 0))
                item.SetItem(ItemGamePlayData.detailObstacleObjects[_slot].itemType, ItemGamePlayData.detailObstacleObjects[_slot].eTypeObject, ItemGamePlayData.detailObstacleObjects[_slot].icon);
            else
                item.SetItem(ItemGamePlayData.detailItemObjects[_slot].itemType, ItemGamePlayData.detailItemObjects[_slot].eTypeObject, ItemGamePlayData.detailItemObjects[_slot].icon);
            
            return item;
        }
        int CheckRateSpawn()
        {

            return GetAllItem((totalItem > 0 && totalItem % itemSpawnPerOnceObstacle == 0) ? ETypeObject.Obstacle : ETypeObject.Item);
            byte GetAllItem(ETypeObject eTypeObject)
            {
                List<byte> _allSoltRate = new List<byte>();
                List<ItemTable> _itemTables = itemTables.Where(item => item.eTypeObject.Equals(eTypeObject)).ToList();
                for (byte i = 0; i < _itemTables.Count; i++)
                {
                    for (byte j = 0; j < (_itemTables[i].percentRate / 10); j++)
                    {
                        _allSoltRate.Add(i);
                    }
                }
                return _allSoltRate[UnityEngine.Random.Range(0, _allSoltRate.Count)];
            }
        }
        public void CheckItemSpawn()
        {
            if (allItems.Count(item => item.gameObject.activeSelf) > 0)
            {
                totalItem = 0;
                itemTables.ForEach(item => item.count = 0);
                allItems.Where(item => item.gameObject.activeSelf).ToList().ForEach(piece =>
                  {
                      itemTables.ForEach(item =>
                      {
                          if (item.nameObject.Equals(piece.nameItem))
                          {
                              item.count++;
                              totalItem++;
                          }
                      });
                  });
            }
        }
        public void CheckItemRemain(string nameItem)
        {
            itemTables.ForEach(item =>
            {
                if (item.nameObject.Equals(nameItem))
                {
                    item.count--;
                    totalItem--;
                }
            });
        }
    }
}
