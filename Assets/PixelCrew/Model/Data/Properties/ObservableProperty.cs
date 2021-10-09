using System;
using PixelCrew.Utils.Disposables;
using TMPro;
using UnityEngine;

namespace PixelCrew.Model.Data.Properties
{   [Serializable]
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;
        
        public delegate void OnPropertyChange(TPropertyType newValue, TPropertyType oldValue);

        public event OnPropertyChange OnChanged;
        
        public IDisposable Subscribe(OnPropertyChange call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public IDisposable SubscribeAndInvoke(OnPropertyChange call)
        {
            OnChanged += call;
            var dispose = new ActionDisposable(() => OnChanged -= call);
            call(_value, _value);
            return dispose;
        }

        
        public virtual TPropertyType Value
        {
            //сохранение не value при измененинии
            get => _value;
            set
            {
                var isSame = _value.Equals(value);
                if(isSame) return;
                var oldValue = _value;
                _value = value;
                InvokeChangedEvent(_value, oldValue);
                
            }
        }

        protected void InvokeChangedEvent(TPropertyType newValue, TPropertyType oldValue)
        {
            OnChanged?.Invoke(newValue, oldValue);
        }
    }
}
