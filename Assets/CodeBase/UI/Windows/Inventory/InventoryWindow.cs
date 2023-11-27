using UnityEngine;

namespace CodeBase.UI.Windows.Inventory
{
    public class InventoryWindow : MonoBehaviour
    {
        [field: SerializeField] public Transform SlotsRoot { get; private set; }
    }
}