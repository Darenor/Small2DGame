using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Defenitions
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;
        public int InventorySize => _inventorySize;

        public int MaxHealth => _maxHealth;

    }
}