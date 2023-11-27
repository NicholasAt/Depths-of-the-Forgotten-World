using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.PlayerTeleport
{
    public class TeleportShowTutorial : BaseShowTutorial
    {
        [SerializeField] private TeleportPlayerAnotherLocation _teleport;
        protected override string Context { get; set; }

        protected override void OnStart()
        {
            Context = _teleport.IsToHole ? "Спуститися - E" : "Піднятися - E";
        }
    }
}