using CodeBase.Services.StaticData;
using CodeBase.StaticData.Audio;
using UnityEngine;

namespace CodeBase.Services.GameSound
{
    public class GameSoundService : IGameSoundService
    {
        private readonly IStaticDataService _dataService;
        private AudiosourceKeeper _audioSource;

        public GameSoundService(IStaticDataService dataService)
        {
            _dataService = dataService;
        }

        public void InitializeAudioSource()
        {
            _audioSource = Object.Instantiate(_dataService.AudioData.AudioSourcePrefab);
        }

        public void PlayBackgroundMusic()
        {
            AudioSource source = _audioSource.BackgroundSource;
            if (source.isPlaying == false)
            {
                source.clip = _dataService.ForAudio(AudioId.BackgroundSound).Clip;
                source.Play();
            }
        }

        public void PlayOneShot(AudioClip clip) =>
            _audioSource.PlayOneShotSource.PlayOneShot(clip);
    }
}