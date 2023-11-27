using CodeBase.StaticData.PlayerTeleport;
using UnityEngine;

namespace CodeBase.Logic.Markers.PlayerTeleport
{
    public class PlayerTeleportSpawnMarker : BaseSpanMarker<TeleportId>
    {
        [SerializeField] private Color _pointColor;
        [field: SerializeField] public Transform TeleportPoint { get; private set; }
        [field: SerializeField] public bool IsTeleportToHole { get; private set; }
        protected override string ObjectName { get; set; } = "Teleport";

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = new Color(_pointColor.r, _pointColor.g, _pointColor.b);
            Gizmos.DrawSphere(TeleportPoint.position, 0.5f);
        }
    }
}