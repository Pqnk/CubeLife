using UnityEngine;

public abstract class PivotCameraBehavior : MonoBehaviour
{
    protected Camera  _camera;
    protected Vector3 _initLocalPosCamera;
    protected Vector3 _pivotPosition;

    private void OnEnable()
    {
        EventManager.OnGridParametersChanged += AdaptCameraParametersToGridSize;
    }

    private void OnDisable()
    {
        EventManager.OnGridParametersChanged -= AdaptCameraParametersToGridSize;
    }


    /// <summary>
    /// Method to adapt the Up Camera to the grid size, so the grid takes all the screen space available.
    /// Called each time the 'EventManager.OnGridParametersChanged' is invoked.
    /// Event method 'EventManager.OnGridParametersChanged' is invoked each time the GridSize slider parameter is changed.
    /// </summary>
    /// <param name="upperRightCellPosition"> Position of the upper Righ celle position in the grid, used to calculate the center of the grid.</param>
    protected abstract void AdaptCameraParametersToGridSize(Vector3 uperRightCellPosition);


    void Start()
    {
        _camera = this.transform.GetChild(0).transform.GetComponent<Camera>();
        _initLocalPosCamera = _camera.gameObject.transform.localPosition;
    }
}
