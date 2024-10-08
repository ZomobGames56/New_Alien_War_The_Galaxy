using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public event Action<bool> ButtonState;
    private bool isButtonPressed;
    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        ButtonState?.Invoke(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        StartCoroutine(buttonPressed());
        ButtonState?.Invoke(true);
    }
    IEnumerator buttonPressed()
    {
        while(isButtonPressed)
        {
            ButtonState?.Invoke(isButtonPressed);
            yield return null;
        }
        StopCoroutine(buttonPressed());
    }
}
