using System;
using UnityEngine;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Logic
{
    public class PlayerTriggerReporter : MonoBehaviour
    {
        public Action OnEnter;
        public Action OnExit;
        public Transform PlayerTransform { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Rigidbody2D attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null && attachedRigidbody.CompareTag(PlayerTag))
            {
                PlayerTransform = attachedRigidbody.transform;
                OnEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Rigidbody2D attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null && attachedRigidbody.CompareTag(PlayerTag))
            {
                OnExit?.Invoke();
            }
        }
    }
}