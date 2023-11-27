using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerInteraction : MonoBehaviour, IFreezePlayer
    {
        private IInputService _inputService;
        private PlayerStaticData _config;

        public void Construct(IInputService inputService, IStaticDataService dataService)
        {
            _inputService = inputService;
            _config = dataService.PlayerData;

            _inputService.OnPlayerInteraction += Interaction;
        }

        private void OnDestroy() =>
            _inputService.OnPlayerInteraction -= Interaction;

        public void Freeze() =>
            _inputService.OnPlayerInteraction -= Interaction;

        public void Unfreeze()
        {
            _inputService.OnPlayerInteraction -= Interaction;
            _inputService.OnPlayerInteraction += Interaction;
        }

        private void Interaction()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _config.InteractRadius);

            foreach (Collider2D collision in colliders)
            {
                if (collision.isTrigger)
                    continue;

                Rigidbody2D attachedRigidbody = collision.attachedRigidbody;
                if (attachedRigidbody != null && attachedRigidbody.TryGetComponent(out IPlayerInteraction interact))
                {
                    interact.Interaction();
                    break;
                }
            }
        }
    }
}