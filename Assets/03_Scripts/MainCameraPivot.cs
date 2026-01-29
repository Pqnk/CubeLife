using UnityEngine;
using UnityEngine.UIElements;

public class MainCameraPivot : PivotCameraBehavior
{
    private float _rotationSpeed = 0.05f;
    private bool _canRotate = false;


    void Update()
    {
        if (_canRotate)
        {
            this.gameObject.transform.Rotate(new Vector3(0f, _rotationSpeed, 0f));
        }
    }

    protected override void AdaptCameraParametersToGridSize(Vector3 uperRightCellPosition)
    {
        Vector3 pivotPosition = uperRightCellPosition / 2;
        this.gameObject.transform.position = pivotPosition;

        float factor = pivotPosition.magnitude > 2 ? 0.05f : 0.2f;

        Vector3 newpos = new Vector3(_initLocalPosCamera.x, _initLocalPosCamera.y * pivotPosition.magnitude * factor, _initLocalPosCamera.z * pivotPosition.magnitude * factor);
        _camera.gameObject.transform.localPosition = newpos;
    }
}
