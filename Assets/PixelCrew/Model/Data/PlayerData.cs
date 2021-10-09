using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrew.Creatures;
using PixelCrew.Components;
using PixelCrew.Model.Data.Properties;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public IntProperty Hp = new IntProperty();
        

        public InventoryData Inventory => _inventory;
        public PlayerData Clone()
        {

            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }

}
