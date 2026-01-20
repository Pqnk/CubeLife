using UnityEngine;

public class UpCameraPivot : MonoBehaviour
{
    [SerializeField] private Vector3 _pivotPosition;
    private Camera _upCamera;
    private float _initialCameraSize;

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
        _pivotPosition = position / 2;
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
