using CodeBase.Player;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Npc.ShopNpc
{
    public class OpenShop : MonoBehaviour, IPlayerInteraction
    {
        private IUIFactory _uiFactory;
        private List<ItemId> _ids;

        public void Construct(List<ItemId> ids, IUIFactory uiFactory)
        {
            _ids = ids;
            _uiFactory = uiFactory;
        }

        public void Interaction()
        {
            Open();
        }

        private void Open()
        {
            _uiFactory.CreateShopWindow(_ids);
        }
    }
}