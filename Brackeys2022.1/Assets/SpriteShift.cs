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
    private PlaneShift planeShift;

    private void Awake()
    {
        planeShift = GameObject.Find("GameManager").GetComponent<PlaneShift>();
    }
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        planeShift.OnShiftToReal?.AddListener(SetSpriteReal);
        planeShift.OnShiftToImaginary?.AddListener(SetSpriteImaginary);
    }

    private void OnDisable()
    {
        planeShift.OnShiftToImaginary?.RemoveListener(SetSpriteImaginary);
        planeShift.OnShiftToReal?.RemoveListener(SetSpriteReal);
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
