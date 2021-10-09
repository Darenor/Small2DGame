using System;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _sfx;

        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;
        
        private static GameSettings _instace;
        public static GameSettings I => _instace == null ? LoadGameSettings() : _instace;

        private static GameSettings LoadGameSettings()
        {
            return _instace = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistentProperty(1, SoundSetting.Music.ToString());
            _sfx = new FloatPersistentProperty(1, SoundSetting.Sfx.ToString());
        }

        private void OnValidate()
        {
            Music.Validate();
            Sfx.Validate();
        }
        
    }
    public enum SoundSetting
    {
        Music,
        Sfx
    }
}
