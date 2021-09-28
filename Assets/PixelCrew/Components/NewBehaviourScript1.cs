/**using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Components
{
    public class MobAi : MonoBehaviour
    {

        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private float _alarmDelay;

        private Coroutine _current;
        private GameObject _target;
        private Animator _animator;
        private bool _isDead;

        //private SpawnListComponent _particles;


        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
        }

        private void Start()
        {
            StartState(Patrolling());
        }

        public void OnHeroInVision(GameObject go)
        {
            _target = go;

            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            _particles.Spawn("Exclamtion");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(_canAttack());

                }
                else
                {
                    SetDirectionTarget();
                }
                
                
                
               
                yield return null;
            }
        }

        private IEnumerator Patrolling()
        {
            yield return null;
        }
        private void StartState(IEnumerator coroutine)
        {
            if (_current != null)
                StopCoroutine(_current);

            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);
        }
    }
}*/