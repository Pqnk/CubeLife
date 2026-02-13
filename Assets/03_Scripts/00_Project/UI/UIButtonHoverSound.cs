using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UISoundType _uiSoundType;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.transform.GetComponent<Button>().interactable)
            ProjectManager.Instance.audioManager.PlayUISound(_uiSoundType);
    }
}
