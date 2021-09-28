using UnityEngine;
using PixelCrew.Components;
using PixelCrew.Utils;

namespace Assets.PixelCrew.Creatures.Mobs
{
    public class ShootingTrapAi : MonoBehaviour
    {
        [SerializeField] public ColliderCheck _vision;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private SpriteAnimation _animation;

        private void Update()
        {
            if (_vision.IsTouchingLayer && _cooldown.IsReady)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            _cooldown.Reset();
            _animation.SetClip("start-attack");
        }

    }
}