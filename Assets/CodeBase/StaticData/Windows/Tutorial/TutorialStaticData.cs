using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Windows.Tutorial
{
    [CreateAssetMenu(menuName = "Static Data/Tutorial Static Data", order = 0)]
    public class TutorialStaticData : ScriptableObject
    {
        public List<string> Context;
    }
}