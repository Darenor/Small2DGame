using UnityEditor;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    public interface ICanAddToInventory
    {
        void AddInInventory(string id, int value);
    }
}