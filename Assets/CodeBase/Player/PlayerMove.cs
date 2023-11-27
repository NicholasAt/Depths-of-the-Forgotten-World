using CodeBase.Services.Input;
using CodeBase.StaticData.Player;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMove : MonoBehaviour, IFreezePlayer
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerAnimation _animation;
        [SerializeField] private Rigidbody2D _rigidbody;

        private IInputService _inputService;
        private PlayerStaticData _config;

        public void Construct(IInputService inputService, PlayerStaticData playerStaticData)
        {
            _inputService = inputService;
            _config = playerStaticData;
            _health.Happened += Dead;
        }

        private void FixedUpdate()
        {
            UpdateMove();
        }

        public void Freeze()
        {
            enabled = false;
            _rigidbody.velocity *= 0;
        }

        public void Unfreeze()
        {
            enabled = true;
        }

        private void UpdateMove()
        {
            _animation.UpdateMove();
            _rigidbody.velocity = _config.Speed * 10 * Time.fixedDeltaTime * _inputService.MoveAxis; //extra speed
        }

        private void Dead()
        {
            Freeze();
            StartCoroutine(DelayRespawn());
        }

        private IEnumerator DelayRespawn()
        {
            yield return new WaitForSeconds(_config.DelayMoveAfterDead);
            Unfreeze();
        }
    }
}