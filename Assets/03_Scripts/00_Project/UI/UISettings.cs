using UnityEngine;

public abstract class UISettings : MonoBehaviour
{
    protected SaveDataGridParameters data;

    public abstract void InitializeAllValueText();


    private void Start()
    {
        if (SaveParametersManager.DoesSaveFileAlreadyExists())
        {
            data = SaveParametersManager.ChargeSavedParametersFile();
        }
    }
}
