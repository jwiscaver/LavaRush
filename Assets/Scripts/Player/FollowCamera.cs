using UnityEngine;

namespace Game.Player
{
    public sealed class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector2 offset = new Vector2(0f, 2f);
        [SerializeField] private float followSmoothTime = 0.15f;

        private Vector3 currentVelocity;

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 desiredPosition = new Vector3(
                target.position.x + offset.x,
                target.position.y + offset.y,
                transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, followSmoothTime);
        }

        private void OnValidate()
        {
            followSmoothTime = Mathf.Max(0.01f, followSmoothTime);
        }
    }
}
