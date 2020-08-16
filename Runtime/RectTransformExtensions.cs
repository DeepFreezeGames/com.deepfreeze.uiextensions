using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{
    public static float Width(this RectTransform rectTransform) => rectTransform.rect.width;
    public static float Height(this RectTransform rectTransform) => rectTransform.rect.height;

    public static void SetWidth(this RectTransform rectTransform, float value)
    {
        rectTransform.sizeDelta = new Vector2(value, rectTransform.sizeDelta.y);
    }

    public static void SetHeight(this RectTransform rectTransform, float value)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, value);
    }

    //public static Vector2 WorldRight(this RectTransform rectTransform) => rectTransform.position
}
