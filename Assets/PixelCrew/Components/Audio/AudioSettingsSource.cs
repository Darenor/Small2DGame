using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsSource : MonoBehaviour
    {
        [SerializeField] private SoundSetting _mode;
        private AudioSource _source;
        private FloatPersistentProperty _model;
        private void Start()
        {
            _source = GetComponent<AudioSource>();
            
            _model = FindProperty();
            _model.OnChanged += OnSoundSettingChanged;
            OnSoundSettingChanged(_model.Value, _model.Value);
        }

        private void OnSoundSettingChanged(float newvalue, float oldValue)
        {
            _source.volume = newvalue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSetting.Music:
                    return GameSettings.I.Music;
                case SoundSetting.Sfx:
                    return GameSettings.I.Sfx;
            }

            throw new ArgumentException("Undefined mode");
        }
    }
}
