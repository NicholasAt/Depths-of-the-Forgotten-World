using UnityEngine;

namespace CodeBase.StaticData.SpawnData
{
    public abstract class BaseSpawnData<TId>
    {
        [SerializeField] private string _inspectorName;

        [field: SerializeField] public TId Id { get; private set; }

        [field: SerializeField] public Vector2 Position { get; private set; }

        protected BaseSpawnData(TId id, Vector2 position)
        {
            Position = position;
            Id = id;
            RefreshName();
        }

        public virtual void OnValidate()
        {
            RefreshName();
        }

        private void RefreshName() =>
            _inspectorName = Id.ToString();
    }
}