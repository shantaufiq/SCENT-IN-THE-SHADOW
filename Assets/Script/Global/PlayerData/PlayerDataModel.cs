using System.Collections.Generic;

namespace ScentInTheShadow.Global.PlayerData
{
    [System.Serializable]
    public class PlayerDataModel 
    {
        public int Health;
        public int Experience;
        public int Skor;
        public int Level;
        public string Gender;
        public List<InventoryData.Item> items;
    }
}
