using UnityEngine;

public class MainCameraPivot : MonoBehaviour
{
    #region ----VARIABLES - CAMERA----
    private Camera _mainCamera;
    private Vector3 _initLocalPosMainCamera;

    private float _rotationSpeed = 0.05f;
    private bool _canRotate = false;
    #endregion


    #region ----EVENT - ADAPTING CAMERA DISTANCE WITH GRID SIZE----
    private void OnEnable()
    {
        EventManager.OnGridParametersChanged += OnGridParametersChanged;
    }

    private void OnDisable()
    {
        EventManager.OnGridParametersChanged -= OnGridParametersChanged;
    }

    private void OnGridParametersChanged(Vector3 position)
    {
        Vector3 pivotPosition = position / 2;
        this.gameObject.transform.position = pivotPosition;

        float factor = pivotPosition.magnitude > 2 ? 0.05f : 0.2f;

        Vector3 newpos = new Vector3(_initLocalPosMainCamera.x, _initLocalPosMainCamera.y * pivotPosition.magnitude * factor, _initLocalPosMainCamera.z * pivotPosition.magnitude * factor);
        _mainCamera.gameObject.transform.localPosition = newpos;
    }

    #endregion

    void Start()
    {
        _mainCamera = this.transform.GetChild(0).transform.GetComponent<Camera>();
        _initLocalPosMainCamera = _mainCamera.gameObject.transform.localPosition;
    }

    void Update()
    {
        if (_canRotate)
        {
            this.gameObject.transform.Rotate(new Vector3(0f, _rotationSpeed, 0f));
        }
    }
}
