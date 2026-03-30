using Unity.Cinemachine;
using UnityEngine;


public enum DirectionEnum
{
    up, down, left, right
}

public class CameraMoving : MonoBehaviour 
{
    [SerializeField] private DirectionEnum _directionEnum;
    [SerializeField] private float _power;
    private CinemachineCamera _camera;
    private CinemachinePositionComposer _cameraPC;

    private void Awake()
    {
        _camera = GameObject.Find("Camera").GetComponentInChildren<CinemachineCamera>();
        _cameraPC = _camera.GetComponent<CinemachinePositionComposer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (_directionEnum)
            {
                case DirectionEnum.up:
                    _cameraPC.Composition.ScreenPosition = new Vector2(0,_power);
                    break;
                    case DirectionEnum.down:
                    _cameraPC.Composition.ScreenPosition = new Vector2(0,-_power);
                    break;
                case DirectionEnum.left:
                    _cameraPC.Composition.ScreenPosition = new Vector2(_power, 0);
                    break;
                case DirectionEnum.right:
                    _cameraPC.Composition.ScreenPosition = new Vector2(-_power, 0);
                    break; default: break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _cameraPC.Composition.ScreenPosition = new Vector2(0,0);
        }
    }
}