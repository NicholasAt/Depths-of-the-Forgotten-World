using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapper;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<Bootstrapper>();
            if (bootstrapper == null)
                Instantiate(_bootstrapper);

            Destroy(gameObject);
        }
    }
}