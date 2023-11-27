using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Action<int> OnChangeSelectSlot { get; set; }
        Action OnUseItem1 { get; set; }
        Action OnUseItem2 { get; set; }
        Action OnPlayerInteraction { get; set; }

        void Cleanup();
    }
}