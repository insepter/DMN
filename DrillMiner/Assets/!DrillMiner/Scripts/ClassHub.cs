namespace Insepter.DM
{
    using System;
    using UnityEngine;
    #region StructAndClass
    // GamePlay //
    [Serializable]
    public class DetailObject<T>
    {
        public T itemType;
        public ETypeObject eTypeObject;
        public Texture2D icon;
    }
    // Upgrade //
    [Serializable]
    public class DetailSkill
    {
        public uint refId;
        public EUpgrade eUpgrade;
        [Space]
        public uint presentLv;
        public Texture2D icon;
    }
    // Shop //
    [Serializable]
    public class DetailShopItem
    {
        public uint refId;
        public Texture2D icon;
        public float cost;
        public bool isUnlock, isUse;
        // Abillity //
        public float speed, speedTurn;
    }
    #endregion

    #region Enum
    // GamePlay //
    public enum ETypeObject { Item, Obstacle }
    public enum EItem { IronOre, BronzeOre, SilverOre, GoldOre, Diamond, Fuel, Booster }
    public enum EObstacle { Ore, BigOre, Bomb }
    // Upgrade //
    public enum EUpgrade
    {
        FuelTank, OilPurifierMachine, AtomPython, PerfectCombustion, ImprovedBoosterSpeed, ImprovedBoosterDuration,
        IronCurtain, IronOrePurifier, BronzeOrePurifier, SilverOrePurifier, GoldOrePurifier, DiamondPurifier
    }
    #endregion

    #region Interface
    // UI //
    public interface ISetAllUI<T>
    {
        void SetAllUI(T item);
    }
    // GamePlay //
    public interface ITypeItem
    {
        string nameItem { get; set; }
        ETypeObject eTypeObject { get; set; }
        void SentItem();
    }
    #endregion
}
