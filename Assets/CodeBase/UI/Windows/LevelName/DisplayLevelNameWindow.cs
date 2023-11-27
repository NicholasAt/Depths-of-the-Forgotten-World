using CodeBase.Services.GameObserver;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.LevelName
{
    public class DisplayLevelNameWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNameText;

        private IGameObserverService _observerService;
        private string _worldName, _holeName;

        public void Construct(in string worldName, in string holeName, IGameObserverService observerService)
        {
            _observerService = observerService;
            _worldName = worldName;
            _holeName = holeName;

            _observerService.OnPlayerTeleport += Refresh;
            Refresh(false);
        }

        private void OnDestroy()
        {
            _observerService.OnPlayerTeleport -= Refresh;
        }

        private void Refresh(bool isToHole) =>
            _levelNameText.text = isToHole ? _holeName : _worldName;
    }
}