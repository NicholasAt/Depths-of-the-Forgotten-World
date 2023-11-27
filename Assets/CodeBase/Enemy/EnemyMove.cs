using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Transform _bodyTransform;
        private float _startLocalScaleX;

        private void Start()
        {
            _startLocalScaleX = _bodyTransform.localScale.x;
        }

        public void Move(Vector2 at, float speed)
        {
            transform.position = Vector2.MoveTowards(transform.position, at, speed * Time.deltaTime);

            Vector3 currentScale = _bodyTransform.localScale;
            currentScale.x = (transform.position.x > at.x) ?
                _startLocalScaleX :
                -_startLocalScaleX;
            _bodyTransform.localScale = currentScale;
        }
    }
}