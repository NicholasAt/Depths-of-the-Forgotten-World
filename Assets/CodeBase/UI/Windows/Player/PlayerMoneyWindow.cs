using CodeBase.Data.Progress;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Player
{
    public class PlayerMoneyWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        private PlayerData _playerData;

        public void Construct(IPersistentProgressService persistentProgressService)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            _playerData.OnMoneyChange += Refresh;
            Refresh();
        }

        private void OnDestroy() =>
            _playerData.OnMoneyChange -= Refresh;

        private void Refresh() =>
            _moneyText.text = $"Гроші: {_playerData.Money}";
    }
}