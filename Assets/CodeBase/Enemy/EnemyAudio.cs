using CodeBase.Services.GameSound;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAudio : MonoBehaviour
    {
        private EnemyConfig _config;
        private IGameSoundService _soundService;

        public void Construct(EnemyConfig config, IGameSoundService soundService)
        {
            _config = config;
            _soundService = soundService;
        }

        public void PlayAttack()
        {
            _soundService.PlayOneShot(_config.AttackClip);
        }
    }
}