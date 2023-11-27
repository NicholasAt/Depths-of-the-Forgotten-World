using CodeBase.StaticData.Reward;

namespace CodeBase.Logic.Markers.Reward
{
    public class RewardSpawnMarker : BaseSpanMarker<RewardId>
    {
        protected override string ObjectName { get; set; } = "Reward";
    }
}