using CodeBase.StaticData.PlayerTeleport;
using System;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.PlayerTeleport
{
    [Serializable]
    public class PlayerTeleportSpawnData : BaseSpawnData<TeleportId>
    {
        [field: SerializeField] public bool IsToHole { get; private set; }
        [field: SerializeField] public Vector2 TeleportPosition { get; private set; }

        public PlayerTeleportSpawnData(TeleportId id, Vector2 position, Vector2 teleportPosition, bool isToHole) : base(id, position)
        {
            TeleportPosition = teleportPosition;
            IsToHole = isToHole;
        }
    }
}