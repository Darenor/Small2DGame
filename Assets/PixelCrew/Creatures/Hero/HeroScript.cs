using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Events;
using PixelCrew.Components;
using PixelCrew.Utils;
using PixelCrew.Model;
using PixelCrew.Model.Data;


namespace PixelCrew.Creatures

{
    public class HeroScript : Creature, ICanAddToInventory
    {
       [SerializeField] private CheckCircleOverlap _interactionCheck;
       [SerializeField] private LayerCheck _wallCheck;

       [SerializeField] private float _slamDownVelocity;
        
       [SerializeField] private Cooldown _throwCooldown;
       [SerializeField] private AnimatorController _armed;
       [SerializeField] private AnimatorController _disarmed;

       [Header("Super Throw")] [SerializeField]
       private Cooldown _superThrowCooldown;
       [SerializeField] private int _superThrowParticles;
       [SerializeField] private float _superThrowDelay;
       [SerializeField] private ProbabilityDropComponent _hitDrop;

        private readonly Collider2D[] _ineractionResult = new Collider2D[1];

        private bool _allowDoubleJump;
        private bool _isOnWall;
        private bool _superThrow; 

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

        private int CoinsCount => _session.Data.Inventory.Count("Coin");

    
        private int SwordCount => _session.Data.Inventory.Count("Sword");

       
        public GameSession _session;
        private HealthComponent _health;
        private float _defaultGravityScale;

        protected override void Awake()
        {
            base.Awake();

            _defaultGravityScale = _rigidbody.gravityScale;
        }

      

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var _health = GetComponent<HealthComponent>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            _health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }



        

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = _direction.x * transform.lossyScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                _rigidbody.gravityScale = 0; 
            }
            else
            {
                _isOnWall = false;
                _rigidbody.gravityScale = _defaultGravityScale;
            }

            _animator.SetBool(IsOnWall, _isOnWall);
        }


        

        protected override float CalculateYVelocity()
        {

            var isJumpingPressing = _direction.y > 0;

            if (_isGrounded || _isOnWall)

            {
                _allowDoubleJump = true;               
            }

            if(!isJumpingPressing && _isOnWall)
            {
                return 0f;
            }

            
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!_isGrounded && _allowDoubleJump && !_isOnWall)
            {
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpSpeed;
            }
             
            return base.CalculateJumpVelocity(yVelocity);
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }    

        public override void TakeDamage()
        {
            base.TakeDamage();
            
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }


        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            _hitDrop.SetCount(numCoinsToDispose);
            _hitDrop.CalculateDrop();
        }


        public void Ineract()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
           if (other.gameObject.IsInLayer(_groundLayer))
            {
              var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    _particles.Spawn("Fall");
                }
                                
            }
        }
        

        public override void Attack()
        {
            
            if (SwordCount <= 0) return;

            base.Attack();         
                    
        }

        

        private void UpdateHeroWeapon()
        {
            _animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
       
        }

        public void OnDoThrow()
        {
            if (_superThrow)
            {
                var numThrows = Mathf.Min(_superThrowParticles, SwordCount - 1);
                StartCoroutine( DoSuperThrow(numThrows));
            }
            else
            {
                ThrowAndRemoveFromInventory();
            }
            _superThrow = false;


        }

       
        private IEnumerator DoSuperThrow(int numThrows)
        {
            for (int i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
            
        }

        private void ThrowAndRemoveFromInventory()
        {
            _particles.Spawn("Throw");
            _session.Data.Inventory.Remove("Sword", 1);
            _sounds.Play("Range");
        }

        public void StartThrowing()
        {
           _superThrowCooldown.Reset();
        }

        public void PerformThrowing()
        {
            if (!_throwCooldown.IsReady && SwordCount <= 1) return;

            if (_superThrowCooldown.IsReady) _superThrow = true;

            _animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();
        }

        public void UsePotion()
        {
            var poitionCount = _session.Data.Inventory.Count("HealthPotion");
            if (poitionCount > 0)
            {
                _health.ModifyHealth(5);
                _session.Data.Inventory.Remove("HealthPotion", 1);
            }
        }

    }

}

