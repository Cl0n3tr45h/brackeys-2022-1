using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteShift : MonoBehaviour
{
    public Sprite RealSprite;
    public Sprite ImaginarySprite;
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpriteReal()
    {
        renderer.sprite = RealSprite;
    }
    public void SetSpriteImaginary()
    {
        renderer.sprite = ImaginarySprite;
    }
}
