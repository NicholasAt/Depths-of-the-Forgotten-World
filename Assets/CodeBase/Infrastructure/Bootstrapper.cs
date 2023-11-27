using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            Game game = new Game(this);
            game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}