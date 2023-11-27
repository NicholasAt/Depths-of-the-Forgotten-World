using System;
using UnityEngine;

namespace CodeBase.StaticData.NPC
{
    [Serializable]
    public class ShopNpcConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public ShopNpcId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}