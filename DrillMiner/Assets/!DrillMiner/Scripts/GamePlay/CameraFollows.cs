namespace Insepter.DM.GamePlay.Cameras
{
    using UnityEngine;
    using Insepter.DM.GamePlay.EarthFloor;

    public class CameraFollows : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public PhysicsMaterial2D[] physicMaterials;
        void Update()
        {
            RaycastHit2D _hit2D = Physics2D.Raycast(transform.position, Vector3.down * 10, 150, 1 << 10);
            if (_hit2D.collider != null)
            {
                System.Array.ForEach(physicMaterials, item =>
                {
                    if (item.name.Equals(_hit2D.collider.sharedMaterial.name))
                    {
                        _hit2D.collider.transform.parent.parent.GetComponent<BackgroundMove>().HideCol();
                        new SubBackgroundSpawn().SpawnBackground(item.name, _hit2D.collider.transform.parent.parent.transform as RectTransform);
                        return;
                    }
                });
            }
        }
        void LateUpdate()
        {
            Camera.main.transform.rotation = Quaternion.identity;
            Camera.main.transform.position = target.position + offset;
        }
        void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, Vector3.down * 10, Color.red);
        }
    }
}
