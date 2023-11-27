using CodeBase.Services.GameSound;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Audio
{
    [CreateAssetMenu(menuName = "Static Data/Audio Static Data", order = 0)]
    public class AudioStaticData : ScriptableObject
    {
        [field: SerializeField] public AudiosourceKeeper AudioSourcePrefab { get; private set; }
        public List<AudioConfig> Configs;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}