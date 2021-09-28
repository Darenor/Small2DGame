using System.Collections;
using System.Collections.Generic;
using PixelCrew.Model.Data;
using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Components.CollectablesComponents
{
    public class CollectorComponent : MonoBehaviour, ICanAddToInventory
    {
        [SerializeField] private List<InventoryItemData> _items = new List<InventoryItemData>();

        public void AddInInventory(string id, int value)
        {
            _items.Add(new InventoryItemData(id) { Value = value });
        }


        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();
            foreach (var inventoryItemData in _items)
            {
                session.Data.Inventory.Add(inventoryItemData.Id, inventoryItemData.Value);
            }

            _items.Clear();
        }
    }
}