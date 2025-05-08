using UnityEngine;
using UnityEngine.EventSystems;

public class Tip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject _panel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _panel.SetActive(false);
    }
}
