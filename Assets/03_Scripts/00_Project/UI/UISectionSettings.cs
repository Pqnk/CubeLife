using UnityEngine;

public abstract class UISectionSettings : MonoBehaviour
{
    public abstract void ChargeDataValue();
    public abstract void InitializeBaseSectionSettingsValue();

    private void Awake()
    {
        if (SaveParametersManager.DoesSaveFileAlreadyExists)
        {
            ChargeDataValue();
        }
        else
        {
            InitializeBaseSectionSettingsValue();
        }
    }
}
