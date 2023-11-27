using CodeBase.Player;
using CodeBase.Services.GameObserver;
using UnityEngine;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Enemy
{
    public class EnemyFindTarget : MonoBehaviour
    {
        public Transform PlayerTransform { get; private set; }
        public PlayerHealth PlayerHealth { get; private set; }
        public PlayerMove PlayerMove { get; private set; }

        private IGameObserverService _observerService;

        public void Construct(IGameObserverService observerService)
        {
            _observerService = observerService;

            _observerService.OnPlayerLose += CleanTarget;
            _observerService.OnPlayerTeleport += _ => CleanTarget();
        }

        private void OnDestroy()
        {
            _observerService.OnPlayerLose -= CleanTarget;
            _observerService.OnPlayerTeleport -= _ => CleanTarget();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Rigidbody2D attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null && attachedRigidbody.CompareTag(PlayerTag))
            {
                PlayerMove = attachedRigidbody.GetComponent<PlayerMove>();

                PlayerTransform = attachedRigidbody.transform;
                PlayerHealth = attachedRigidbody.GetComponent<PlayerHealth>();
            }
        }

        private void CleanTarget()
        {
            PlayerTransform = null;
            PlayerHealth = null;
            PlayerMove = null;
        }
    }
}