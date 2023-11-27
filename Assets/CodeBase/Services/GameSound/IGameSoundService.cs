using UnityEngine;

namespace CodeBase.Services.GameSound
{
    public interface IGameSoundService : IService
    {
        void InitializeAudioSource();

        void PlayBackgroundMusic();

        void PlayOneShot(AudioClip clip);
    }
}