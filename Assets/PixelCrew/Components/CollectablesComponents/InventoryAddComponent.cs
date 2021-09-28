using System.Collections;
using UnityEngine;
using PixelCrew.Creatures;
using PixelCrew.Model.Defenitions;
using PixelCrew.Model.Data;
using PixelCrew.Utils;


namespace PixelCrew.Components.CollectablesComponents
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<ICanAddToInventory>();
            hero?.AddInInventory(_id, _count);
        }

    }
}