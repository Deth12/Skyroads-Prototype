using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, 
    IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public bool Interactable;
    [SerializeField] private RectTransform rect = null;
    
    [Header("Colors")]
    public Color NormalColor;
    public Color PressedColor;
    public Color HoverColor;
    
    public float TransitionTime = 0.1f;
    
    [Header("Events")]
    public bool Interactable = true;
    public UnityEvent OnClick;
    
    private void Start()
    {
        if(rect == null)
            rect = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.color(rect, PressedColor, TransitionTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.color(rect, NormalColor, TransitionTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Interactable)
            OnClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.color(rect, HoverColor, TransitionTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.color(rect, NormalColor, TransitionTime);
    }
}
