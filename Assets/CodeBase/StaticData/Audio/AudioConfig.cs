using System;
using UnityEngine;

namespace CodeBase.StaticData.Audio
{
    [Serializable]
    public class AudioConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public AudioId Id { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}