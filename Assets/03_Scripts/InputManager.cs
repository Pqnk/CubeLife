using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                GameObject clickedObject = hitInfo.collider.gameObject;              
             
                if (clickedObject.TryGetComponent<CellBehavior>(out CellBehavior cellClicked) && !GameManager.Instance.GameStarted)
                {
                    OnClickOnCell(cellClicked, hitInfo);
                }

            }
        }
    }

    private void OnClickOnCell(CellBehavior cellClicked, RaycastHit hitInfo)
    {
        cellClicked.SetCellState(!hitInfo.collider.gameObject.GetComponentInParent<CellBehavior>().IsAlive);
    }

}
