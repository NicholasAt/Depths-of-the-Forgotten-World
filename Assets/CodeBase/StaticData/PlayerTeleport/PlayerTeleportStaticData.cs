using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.PlayerTeleport
{
    [CreateAssetMenu(menuName = "Static Data/Teleport Static Data", order = 0)]
    public class PlayerTeleportStaticData : ScriptableObject
    {
        public List<TeleportConfig> TeleportConfigs;

        private void OnValidate()
        {
            TeleportConfigs.ForEach(x => x.OnValidate());
        }
    }
}