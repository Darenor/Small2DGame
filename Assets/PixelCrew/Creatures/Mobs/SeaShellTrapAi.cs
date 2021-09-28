using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrew.Components;
using PixelCrew.Utils;
using System;

namespace PixelCrew.Creatures
{
    public class SeaShellTrapAi : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;

        [Header ("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;

        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;

        private static readonly int Range = Animator.StringToHash("range");
        private static readonly int Melee = Animator.StringToHash("melee");

        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if(_vision.IsTouchingLayer)
            {
                if(_meleeCanAttack.IsTouchingLayer)
                {
                    if (_meleeCooldown.IsReady)
                        MeleeAttack();
                    return;
                }

                if (_rangeCooldown.IsReady)
                    RangeAttack();
            }

            
        }

        private void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }

        private void MeleeAttack()
        {
            _meleeCooldown.Reset();
            _animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
        
    }
}

