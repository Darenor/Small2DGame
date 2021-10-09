using System;
using UnityEngine;

namespace PixelCrew.Model.Defenitions
{
    [CreateAssetMenu(menuName = "Defs/PotionItems", fileName = "PotionItems")]
    public class PotionItemDef : ScriptableObject
    {
        [SerializeField] private PotionDef[] _items;

        public PotionDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }
            
            return default;
        }
    }
    
    
    [Serializable]
    public struct PotionDef
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private float _value;
        [SerializeField] private float _time;


        public string Id => _id;
        public float Value => _value;
        public float Time => _time;
    }
}
