using CodeBase.StaticData.SpawnData.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Markers.Enemy
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public EnemySpawnConfig[] Enemys { get; private set; }
        [field: SerializeField] public float SpawnRadius { get; private set; }

        private void OnValidate()
        {
            foreach (var data in Enemys)
                data.OnValidate();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SpawnRadius);
        }
    }
}