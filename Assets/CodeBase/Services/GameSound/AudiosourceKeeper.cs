using UnityEngine;

namespace CodeBase.Services.GameSound
{
    public class AudiosourceKeeper : MonoBehaviour
    {
        [field: SerializeField] public AudioSource PlayOneShotSource { get; private set; }
        [field: SerializeField] public AudioSource BackgroundSource { get; private set; }
    }
}