using UnityEngine;

public class UpCameraPivot : MonoBehaviour
{
    private Vector3 _pivotPosition;
    private Camera _upCamera;
    private float _initialCameraSize;

    #region ----SUBSCRIPTION - EventManager.OnGridParametersChanged----
    private void OnEnable()
    {
        EventManager.OnGridParametersChanged += AdaptUpCameraParametersToGridSize;
    }

    private void OnDisable()
    {
        EventManager.OnGridParametersChanged -= AdaptUpCameraParametersToGridSize;
    }
    #endregion

    /// <summary>
    /// Method to adapt the Up Camera to the grid size, so the grid takes all the screen space available.
    /// Method called each time the 'EventManager.OnGridParametersChanged' is invoked.
    /// 'EventManager.OnGridParametersChanged' is invoked each time the GridSize slider parameter is changed.
    /// </summary>
    /// <param name="upperRightCellPosition">Position of the upper Righ celle position in the grid, used to calculate the center of the grid.</param>
    private void AdaptUpCameraParametersToGridSize(Vector3 upperRightCellPosition)
    {
        _pivotPosition = upperRightCellPosition / 2;
        this.gameObject.transform.position = _pivotPosition;

        if(_pivotPosition.magnitude > 1)
            _upCamera.orthographicSize = _pivotPosition.magnitude*0.8f;
    }


    void Start()
    {
        _upCamera = this.transform.GetChild(0).transform.GetComponent<Camera>();
        _initialCameraSize = _upCamera.orthographicSize;
    }
}
