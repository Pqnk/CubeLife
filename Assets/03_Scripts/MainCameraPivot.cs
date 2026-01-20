using UnityEngine;

public class MainCameraPivot : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.05f;
    [SerializeField] private Vector3 _pivotPosition;
    private Camera _mainCamera;
    private Vector3 _initLocalPosMainCamera;



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

        float factor = _pivotPosition.magnitude > 2 ? 0.05f : 0.2f;

        Vector3 newpos = new Vector3(_initLocalPosMainCamera.x, _initLocalPosMainCamera.y * _pivotPosition.magnitude * factor, _initLocalPosMainCamera.z * _pivotPosition.magnitude * factor);
        _mainCamera.gameObject.transform.localPosition = newpos;
    }

    void Start()
    {
        _mainCamera = this.transform.GetChild(0).transform.GetComponent<Camera>();
        _initLocalPosMainCamera = _mainCamera.gameObject.transform.localPosition;
    }

    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0f, _rotationSpeed, 0f));
    }
}
