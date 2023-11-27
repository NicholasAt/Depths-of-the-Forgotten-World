using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Windows.Tutorial;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CodeBase.Data.GameConstants;

namespace CodeBase.UI.Windows.Tutorial
{
    public class TutorialWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _contextText;
        [SerializeField] private Button _nextButton;

        private TutorialStaticData _config;
        private int _currentIndex = -1;
        private IGameStateMachine _gameStateMachine;

        public void Construct(IStaticDataService dataService, IGameStateMachine gameStateMachine)
        {
            _config = dataService.TutorialData;
            _gameStateMachine = gameStateMachine;
            _nextButton.onClick.AddListener(Next);

            Next();
        }

        private void Next()
        {
            _currentIndex++;
            if (_currentIndex >= _config.Context.Count)
                _gameStateMachine.Enter<LoadLevelState, string>(FirstLevelSceneLey);
            else
                _contextText.text = _config.Context[_currentIndex];
        }
    }
}