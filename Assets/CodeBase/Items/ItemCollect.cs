using CodeBase.Data.Items;
using CodeBase.Logic;
using CodeBase.Logic.Infrastructure;
using CodeBase.Services.GameSound;
using CodeBase.Services.LogicFactory;
using UnityEngine;

namespace CodeBase.Items
{
    public class ItemCollect : MonoBehaviour
    {
        [SerializeField] private PlayerTriggerReporter _triggerReporter;

        private InventorySlotsHandler _slotsHandler;
        private BaseItemInventoryData _itemData;
        private AudioClip _collectedClip;
        private IGameSoundService _gameSoundService;

        public void Construct(ILogicFactory logicFactory, AudioClip collectedClip, IGameSoundService gameSoundService, BaseItemInventoryData itemData)
        {
            _slotsHandler = logicFactory.InventorySlotsHandler;
            _itemData = itemData;
            _gameSoundService = gameSoundService;
            _collectedClip = collectedClip;

            Invoke(nameof(SubscribeCollect), 0.6f);//delay
        }

        private void SubscribeCollect() =>
            _triggerReporter.OnEnter += TryCollect;

        private void TryCollect()
        {
            if (_slotsHandler.IsInventoryFull() == false)
            {
                _triggerReporter.OnEnter -= TryCollect;
                _slotsHandler.AddOrDropItem(_itemData);
                _gameSoundService.PlayOneShot(_collectedClip);
                Destroy(gameObject);
            }
        }
    }
}