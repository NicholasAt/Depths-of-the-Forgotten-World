using System;
using UnityEngine;

namespace CodeBase.StaticData.Reward
{
    [Serializable]
    public class RewardConfig
    {
        [SerializeField] private string _inspectorName;

        [field: SerializeField] public RewardId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}