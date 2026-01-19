using UnityEngine;

public class MainCameraPivot : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.25f;


    void Start()
    {
        
    }

    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0f, _rotationSpeed, 0f));
    }
}
