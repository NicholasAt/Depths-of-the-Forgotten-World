using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        public Vector2 MoveAxis => _mainControls.Player.Move.ReadValue<Vector2>();
        public Action OnPlayerInteraction { get; set; }
        public Action OnUseItem1 { get; set; }
        public Action OnUseItem2 { get; set; }
        public Action<int> OnChangeSelectSlot { get; set; }
        private readonly MainControls _mainControls;

        public InputService()
        {
            _mainControls = new MainControls();
            EnableAction();

            InitUseItems();
            InitInventory();
            InitPlayer();
        }

        public void Cleanup()
        {
            OnChangeSelectSlot = null;
            OnPlayerInteraction = null;
            OnUseItem1 = null;
            OnUseItem2 = null;
        }

        private void InitPlayer()
        {
            _mainControls.Player.Interaction.performed += _ => OnPlayerInteraction?.Invoke();
        }

        private void InitInventory()
        {
            _mainControls.Inventory.ChangeSelectSlot.performed += index => OnChangeSelectSlot?.Invoke((int)index.ReadValue<float>());
        }

        private void InitUseItems()
        {
            _mainControls.UseItem.UseItem1.performed += _ => OnUseItem1?.Invoke();
            _mainControls.UseItem.UseItem2.performed += _ => OnUseItem2?.Invoke();
        }

        private void EnableAction()
        {
            _mainControls.Player.Enable();
            _mainControls.Inventory.Enable();
            _mainControls.UseItem.Enable();
        }
    }
}