using UnityEngine;

public class Tween_FlickText : MonoBehaviour
{
    [SerializeField] private float flickDuration = 2f;
    private RectTransform rect;
    
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        LeanTween.alphaText(rect, 0f, flickDuration).setLoopPingPong();
    }
}
