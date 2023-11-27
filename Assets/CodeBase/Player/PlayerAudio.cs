using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAudio : MonoBehaviour, IFreezePlayer
    {
        [SerializeField] private AudioSource _footstepsSource;
        [SerializeField] private AudioSource _playOneShotSource;
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerHealth _health;

        private IInputService _inputService;
        private PlayerStaticData _config;

        public void Construct(IStaticDataService dataService, IInputService inputService)
        {
            _config = dataService.PlayerData;
            _inputService = inputService;
            _footstepsSource.clip = _config.FootstepsClip;
            _health.Happened += Happened;
        }

        private void Update()
        {
            UpdateMove();
        }

        public void Freeze()
        {
            enabled = false;
            _footstepsSource.clip = null;
        }

        public void Unfreeze()
        {
            enabled = true;
            _footstepsSource.clip = _config.FootstepsClip;
        }

        public void PlayAttack()
        {
            _playOneShotSource.PlayOneShot(_config.AttackClip);
        }

        public void PlayDie()
        {
            _playOneShotSource.PlayOneShot(_config.DieClip);
        }

        private void UpdateMove()
        {
            PlayFootsteps(_inputService.MoveAxis != Vector2.zero);
        }

        private void PlayFootsteps(bool isPlay)
        {
            if (isPlay && _footstepsSource.isPlaying == false)
                _footstepsSource.Play();
            else if (isPlay == false && _footstepsSource.isPlaying)
                _footstepsSource.Stop();
        }

        private void Happened()
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