﻿using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Wigdets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        
        [SerializeField] private Image _bar;
        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}
