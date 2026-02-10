using UnityEngine;

public class DoNotDestroyOnLoadManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
