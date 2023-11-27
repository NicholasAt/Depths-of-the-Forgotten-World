using CodeBase.PlayerTeleport;
using System;
using UnityEngine;

namespace CodeBase.StaticData.PlayerTeleport
{
    [Serializable]
    public class TeleportConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public TeleportId Id { get; private set; }
        [field: SerializeField] public TeleportPlayerAnotherLocation Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}