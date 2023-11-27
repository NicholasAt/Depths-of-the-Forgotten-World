using TMPro;
using UnityEngine;

namespace CodeBase.Logic
{
    public abstract class BaseShowTutorial : MonoBehaviour
    {
        protected abstract string Context { get; set; }
        [SerializeField] private PlayerTriggerReporter _triggerReporter;
        [SerializeField] private GameObject _tutorialObject;
        [SerializeField] private TMP_Text _tutorialText;

        private void Start()
        {
            Hide();

            _triggerReporter.OnEnter += Show;
            _triggerReporter.OnExit += Hide;
            OnStart();
        }

        private void Show()
        {
            _tutorialText.text = Context;
            _tutorialObject.SetActive(true);
        }

        private void Hide()
        {
            _tutorialObject.SetActive(false);
        }

        protected virtual void OnStart()
        { }
    }
}