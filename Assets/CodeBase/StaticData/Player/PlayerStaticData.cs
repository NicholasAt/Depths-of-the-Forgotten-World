using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player Static Data", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; } = 10f;
        [field: SerializeField] public int Money { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float StairSpeed { get; private set; } = 3f;
        [field: SerializeField] public float DurationOfImmortality { get; private set; } = 1f;
        [field: SerializeField] public float DelayMoveAfterDead { get; private set; } = 2f;
        [field: SerializeField] public float DropItemsRadius { get; private set; } = 1f;
        [field: SerializeField] public float InteractRadius { get; private set; } = 2f;
        [field: SerializeField] public AudioClip DieClip { get; private set; }
        [field: SerializeField] public AudioClip FootstepsClip { get; private set; }
        [field: SerializeField] public AudioClip MoveStairClip { get; private set; }
        [field: SerializeField] public AudioClip AttackClip { get; private set; }
    }
}