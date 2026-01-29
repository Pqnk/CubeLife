using UnityEngine;

public class UpCameraPivot : PivotCameraBehavior
{
    protected override void AdaptCameraParametersToGridSize(Vector3 uperRightCellPosition)
    {
        _pivotPosition = uperRightCellPosition / 2;
        this.gameObject.transform.position = _pivotPosition;

        if (_pivotPosition.magnitude > 1)
            _camera.orthographicSize = _pivotPosition.magnitude * 0.8f;
    }
}
