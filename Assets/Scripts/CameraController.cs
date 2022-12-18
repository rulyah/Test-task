using Configs;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private float _rotX;
    private float _rotY;
    private float _currentRotX;
    private float _currentRotY;
    private float _velocityX;
    private float _velocityY;

    private void Update()
    {
        _rotX += Input.GetAxis("Mouse Y") * GameConfig.instance.cameraSensitivity;
        _rotY += Input.GetAxis("Mouse X") * GameConfig.instance.cameraSensitivity;;
        _rotX = Mathf.Clamp(_rotX, GameConfig.instance.cameraVerticalMin, GameConfig.instance.cameraVerticalMax);
        _currentRotX = Mathf.SmoothDamp(_currentRotX, _rotX, 
            ref _velocityX, GameConfig.instance.cameraSmooth);
        
        _currentRotY = Mathf.SmoothDamp(_currentRotY, _rotY,
            ref _velocityY, GameConfig.instance.cameraSmooth);
        
        _camera.transform.rotation = Quaternion.Euler(-_rotX, _rotY, 0.0f);
    }
}