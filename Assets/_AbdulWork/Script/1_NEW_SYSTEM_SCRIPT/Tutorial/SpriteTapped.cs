using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SpriteTapped : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent spriteEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(spriteEvent != null)
        {
            spriteEvent.Invoke();
        }
    }
}
