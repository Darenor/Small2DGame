using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Model.Defenitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef _items;
        [SerializeField] private ThrowableItemDef _throwableItems;
        [SerializeField] private PotionItemDef _potionItems;
        [SerializeField] private PlayerDef _player;

        public InventoryItemDef Items => _items;

        public ThrowableItemDef Throwable => _throwableItems;
        public PotionItemDef Potion => _potionItems;

        
        public PlayerDef Player => _player;

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }

}
