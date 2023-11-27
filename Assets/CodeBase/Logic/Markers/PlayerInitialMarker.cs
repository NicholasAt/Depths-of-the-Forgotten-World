﻿using UnityEngine;

namespace CodeBase.Logic.Markers
{
    public class PlayerInitialMarker : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}