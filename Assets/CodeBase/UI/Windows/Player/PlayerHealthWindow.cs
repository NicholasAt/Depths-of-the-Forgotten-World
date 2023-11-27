using CodeBase.Data.Progress;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Player
{
    public class PlayerHealthWindow : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;
        private PlayerData _playerData;
        private PlayerStaticData _config;

        public void Construct(IPersistentProgressService persistentProgressService, IStaticDataService dataService)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            _config = dataService.PlayerData;
            _playerData.OnHealthChange += Refresh;
            Refresh();
        }

        private void OnDestroy()
        {
            _playerData.OnHealthChange -= Refresh;
        }

        private void Refresh()
        {
            _healthImage.fillAmount = _playerData.CurrentHealth / _config.Health;
        }
    }
}