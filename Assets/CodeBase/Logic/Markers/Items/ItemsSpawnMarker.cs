using CodeBase.StaticData.Items;

namespace CodeBase.Logic.Markers.Items
{
    public class ItemsSpawnMarker : BaseSpanMarker<ItemId>
    {
        protected override string ObjectName { get; set; } = "Item";
    }
}