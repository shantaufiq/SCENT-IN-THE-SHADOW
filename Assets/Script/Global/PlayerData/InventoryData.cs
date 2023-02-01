using System.Collections.Generic;
using UnityEngine;

namespace ScentInTheShadow.Global.PlayerData
{
    [CreateAssetMenu]
    [System.Serializable]
    public class InventoryData : ScriptableObject
    {
        [System.Serializable]
        public struct Item
        {
            public int item_id;
            public string name;
            public int qty;
        }

        public List<Item> items;    

    }
}