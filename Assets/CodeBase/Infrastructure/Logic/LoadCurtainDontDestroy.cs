using UnityEngine;

namespace CodeBase.Infrastructure.Logic
{
    public class LoadCurtainDontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}