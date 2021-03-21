namespace Common.Camera
{
    using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float SmoothTime = 0.3f;
        Vector3 _velocity = Vector3.zero;
        void Start() => offset = transform.position - target.position;
        void LateUpdate()
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref _velocity, SmoothTime);
            transform.LookAt(target.position);
        }
    }
}
