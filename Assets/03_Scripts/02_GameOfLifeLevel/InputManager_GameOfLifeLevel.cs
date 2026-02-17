using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager_GameOfLifeLevel : MonoBehaviour
{
    private Camera _mainCamera;
    private CellBehavior _cellClicked;
    private bool rightClicking;
    private Vector2 lookDelta;
    private float sensitivity = 0.1f;
    private float minPitch = -80f;
    private float maxPitch = 80f;
    private float yaw;  
    private float pitch;
    private Vector2 clickStartPos;
    private Vector2 currentMousePos;

    [SerializeField] private GameObject _camPivot;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }


    #region ----LeftClick----
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                GameObject clickedObject = hitInfo.collider.gameObject;

                if (clickedObject.TryGetComponent<CellBehavior>(out CellBehavior cellClicked) && !Manager_GameOfLifeLevel.Instance.GameStarted)
                {
                    OnLeftClickOnCell(cellClicked, hitInfo);
                }

            }
        }
    }

    private void OnLeftClickOnCell(CellBehavior cellClicked, RaycastHit hitInfo)
    {
        if (_cellClicked != cellClicked && _cellClicked != null)
            _cellClicked.HighLightNeighbors(false);

        _cellClicked = cellClicked;
        _cellClicked.HighLightNeighbors(true);

        _cellClicked.SetCellState(!hitInfo.collider.gameObject.GetComponentInParent<CellBehavior>().IsAlive);
    }
    #endregion

    #region ----Right Click Hold----
    public void OnRightClickHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rightClicking = context.ReadValueAsButton();
            clickStartPos = currentMousePos;
        }
        else if(context.canceled)
        {
            rightClicking = false;
        }
    }
    #endregion

    #region ----MousePosition----
    public void OnMousePosition(InputAction.CallbackContext context)
    {
        currentMousePos = context.ReadValue<Vector2>();
    }
    #endregion


    private void Update()
    {
        if (rightClicking)
        {
            Vector2 delta = currentMousePos - clickStartPos;

            if (delta.sqrMagnitude < 1f)
                return;

            yaw += delta.x * sensitivity * Time.deltaTime;
            pitch -= delta.y * sensitivity * Time.deltaTime;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            _camPivot.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
