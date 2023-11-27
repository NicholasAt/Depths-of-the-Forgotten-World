using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraFollowTarget : MonoBehaviour
    {
        private Transform _target;

        public void SetTarget(Transform target) =>
            _target = target;

        private void LateUpdate()
        {
            if (_target != null)
                transform.position = _target.position + new Vector3(0, -1f, -10f);//offset
        }
    }
}