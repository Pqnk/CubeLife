using UnityEngine;

public abstract class UISectionSettings : MonoBehaviour
{
    public abstract void ChargeDataValue();
    public abstract void InitializeBaseSectionSettingsValue();

    private void Awake()
    {
        if (SaveSettingsManager.DoesSaveFileAlreadyExists)
        {
            ChargeDataValue();
        }
        else
        {
            InitializeBaseSectionSettingsValue();
        }
    }
}
