using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrew.Creatures;
using PixelCrew.Model.Data;

namespace PixelCrew
{

    public class HeroInputVisual : MonoBehaviour
    {

        [SerializeField] private HeroScript _hero;

        private HeroImputAction _inputActions;

        private void Awake()
        {

            _inputActions = new HeroImputAction();
            _inputActions.Hero.Movement.performed += Movement;
            _inputActions.Hero.Movement.canceled += Movement;                        
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        public void Movement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }        

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Ineract();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _hero.StartThrowing();             
            }

            if (context.canceled)
            {
                _hero.PerformThrowing();
            }
        }
        
        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.UsePotion();
        }


    }

}

    