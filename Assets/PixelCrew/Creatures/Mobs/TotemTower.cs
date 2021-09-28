using System.Collections.Generic;
using UnityEngine;
using PixelCrew.Utils;
using PixelCrew.Components;
using System.Linq;

namespace Assets.PixelCrew.Creatures.Mobs
{
    public class TotemTower : MonoBehaviour
    {
        [SerializeField] private List<ShootingTrapAi> _traps;
        [SerializeField] private Cooldown _cooldown;

        private int _currentTrap;

        private void Start()
        {
            foreach (var shootingTraAi in _traps)
            {
                shootingTraAi.enabled = false;
                var hp = shootingTraAi.GetComponent<HealthComponent>();
                hp._onDie.AddListener(() => OnTrapDead(shootingTraAi));
            }
        }
        //Удаление сломаной ловушки из списка
        private void OnTrapDead(ShootingTrapAi shootingTrapAi)
        {
            var index = _traps.IndexOf(shootingTrapAi);
            _traps.Remove(shootingTrapAi);
            if (index < _currentTrap)
            {
                _currentTrap--;
            }
        }

        private void Update()
        {
            if (_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }
            var hasAnyTraget = _traps.Any(x => x._vision.IsTouchingLayer);
            if (hasAnyTraget)
            {
                if(_cooldown.IsReady)
                {
                    //Переход на следю ловушку
                    _traps[_currentTrap].Shoot();
                    _currentTrap = (int)Mathf.Repeat(_currentTrap + 1, _traps.Count);
                }
            }
        }
    }
}