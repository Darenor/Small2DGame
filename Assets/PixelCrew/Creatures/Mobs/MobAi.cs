using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrew.Components;

namespace PixelCrew.Creatures
{
    public class MobAi : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _missHeroCooldown = 0.5f;
               

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        public string currentState;
        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private IEnumerator _current;
        private bool _isDead;


        private GameObject _target;
        private Patrol _patrol;

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }


        private void Start()
        {
            ChangeStateTo(_patrol.DoPatrol());
        }

        private void ChangeStateTo(IEnumerator corutine)
        {
            currentState = corutine.ToString();
            _creature.SetDirection(Vector2.zero);
                if (_current != null)
                {
                    StopCoroutine(_current);
                }

            _current = corutine;
            StartCoroutine(corutine);

        }


        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;
            _target = go;

            ChangeStateTo(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            ChangeStateTo(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    ChangeStateTo(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                
                yield return null;
            }

            _particles.Spawn("MissHero");
            yield return new WaitForSeconds(_missHeroCooldown);
            _creature.SetDirection(Vector2.zero);
            ChangeStateTo(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            ChangeStateTo(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();            
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }
               
        

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);

            if (_current != null)
                StopCoroutine(_current);
        }

    }

}
