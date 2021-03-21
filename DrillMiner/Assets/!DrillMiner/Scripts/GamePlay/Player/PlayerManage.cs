namespace Insepter.DM.GamePlay.Player
{
    using Insepter.DM.GamePlay.EarthFloor;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerManage : MonoBehaviour
    {
        // Player //
        public RawImage playerRImg;
        // GetIronCurtain //
        public static System.Action<bool> callBackShield { get; private set; }
        public GameObject shield;
        Vector2 _rotZ = new Vector2(-35, 35);
        float _speedRotDrill = 4;
        void Start()
        {
            callBackShield = result => shield.SetActive(result);
            GamePlayController.instance.callBackDitance = () => -transform.position.y / 2.5f;
            
            playerRImg.texture = GamePlayController.instance.shopItemData.GetUseTexture();
            playerRImg.SetNativeSize();
            _speedRotDrill = GamePlayController.instance.shopItemData.GetUse().speedTurn;
        }
        void Update()
        {
            if (GamePlayController.instance.isStartGame)
            {
                Vector2 _pos = (transform as RectTransform).anchoredPosition;
                _pos += new Vector2(GamePlayController.instance.directSpeed / 2, -1f) * Time.deltaTime * GamePlayController.instance.GetSpeed(.1f);
                (transform as RectTransform).anchoredPosition = _pos;

                if (Input.GetAxis("Horizontal") != 0)
                {

                    if (GamePlayController.instance.speedMove > 0)
                        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, GamePlayController.instance.sizeCamera.minValue + (GamePlayController.instance.RemainEnergy() * 10), Time.deltaTime * 5);

                    float _speed = Mathf.Clamp(GamePlayController.instance.GetSpeed(), 0, 300);
                    float _z = Input.GetAxis("Horizontal") * Time.deltaTime * (GamePlayController.instance.isInverst ? -_speedRotDrill : _speedRotDrill) * _speed;
                    Vector3 rot = transform.rotation.eulerAngles + new Vector3(0, 0, _z);
                    rot.z = ClampAngle(rot.z, _rotZ.x, _rotZ.y);
                    transform.eulerAngles = rot;

                    if (transform.eulerAngles.z != 0)
                    {
                        float _warpAnglez = WrapAngle(transform.eulerAngles.z);
                        GamePlayController.instance.directSpeed = _warpAnglez > 0 ? _warpAnglez / _rotZ.y : -Mathf.Abs(_warpAnglez / _rotZ.x);
                    }
                    else
                        GamePlayController.instance.directSpeed = 0;

                }
                //SetCamera();
            }
            //else
            //{
            //    Quaternion targetRotation = Quaternion.Euler(Vector3.zero);
            //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * (EarthFloor.BackgroundController.instance.speedMove / (EarthFloor.BackgroundController.instance.speedMove / 10)));
            //}
        }
        //void SetCamera()
        //{
        //    float _target = Camera.main.orthographicSize + (_isZoom ? -Time.deltaTime : Time.deltaTime) * 10;
        //    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _target, Time.deltaTime * 10);

        //    if (Camera.main.orthographicSize >= 60)
        //        _isZoom = true;
        //    else if (Camera.main.orthographicSize <= 50)
        //        _isZoom = false;
        //}
        float ClampAngle(float angle, float from, float to)
        {
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max(angle, 360 + from);
            return Mathf.Min(angle, to);
        }
        float WrapAngle(float angle)
        {
            angle %= 360;
            if (angle > 180)
                return angle - 360;

            return angle;
        }
        void OnTriggerEnter2D(Collider2D collision) => collision.GetComponent<ITypeItem>()?.SentItem();
    }
}
