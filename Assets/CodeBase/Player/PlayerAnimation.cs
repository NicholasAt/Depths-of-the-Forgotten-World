using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimation : MonoBehaviour, IFreezePlayer
    {
        private readonly int MoveHash = Animator.StringToHash("Move");
        private readonly int ExitDieHash = Animator.StringToHash("ExitDie");
        private readonly int EnterDieTriggerHash = Animator.StringToHash("EnterDieTrigger");

        [SerializeField] private PlayerHealth _health;

        [SerializeField] private Transform _bodyTransform;

        /// <summary>
        /// 0 index need player!
        /// </summary>
        [SerializeField] private Animator[] _animations;

        private IInputService _inputService;
        private PlayerStaticData _config;

        private float _startScaleX;

        public void Construct(IInputService inputService, IStaticDataService dataService)
        {
            _inputService = inputService;
            _startScaleX = _bodyTransform.localScale.x;
            _config = dataService.PlayerData;
            _health.Happened += PlayerDead;
        }

        private void Update()
        {
            UpdateDirection();
        }

        public void Freeze()
        {
            enabled = false;
        }

        public void Unfreeze()
        {
            enabled = true;
        }

        public void PlayAnimation(string animationName)
        {
            foreach (Animator animator in _animations)
                animator.Play(animationName, 0, 0);
        }

        public void UpdateMove()
        {
            foreach (Animator animator in _animations)
            {
                animator.SetBool(MoveHash, _inputService.MoveAxis != Vector2.zero);
            }
        }

        private void UpdateDirection()
        {
            if (_inputService.MoveAxis.x != 0)
            {
                Vector3 currentScale = _bodyTransform.localScale;
                currentScale.x = _inputService.MoveAxis.x > 0 ?
                     -_startScaleX :
                     _startScaleX;
                _bodyTransform.localScale = currentScale;
            }
        }

        private void PlayerDead()
        {
            Freeze();
            foreach (Animator animator in _animations)
            {
                animator.SetTrigger(EnterDieTriggerHash);
                animator.SetBool(ExitDieHash, false);
            }
            StartCoroutine(DelayRespawn());
        }

        private IEnumerator DelayRespawn()
        {
            yield return new WaitForSeconds(_config.DelayMoveAfterDead);
            foreach (Animator animator in _animations)
                animator.SetBool(ExitDieHash, true);

            Unfreeze();
        }
    }
}