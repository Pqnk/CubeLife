using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.transform.GetComponent<Button>().interactable)
            CubeLyfeManager.Instance.audioManager.PlayUISound(UISoundType.Click04);
    }
}
