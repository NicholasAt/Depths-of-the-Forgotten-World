using UnityEngine;

namespace CodeBase.Logic.Markers
{
    public abstract class BaseSpanMarker<TId> : MonoBehaviour
    {
        [field: SerializeField] public TId ID { get; private set; }
        [SerializeField] private Color _color = Color.green;
        [SerializeField] private float _radius = 0.5f;
        protected abstract string ObjectName { get; set; }

        protected virtual void OnValidate()
        {
            if (IsPrefab() == false)
                gameObject.name = $"Marker {ObjectName}: {ID}";
        }

        private bool IsPrefab() =>
            gameObject.scene.rootCount == 0;

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = new Color(_color.r, _color.g, _color.b);
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}