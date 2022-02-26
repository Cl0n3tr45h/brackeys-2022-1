using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootOperand : MonoBehaviour
{
    public ComplexOperand Operand;
    public Image Sprite;
    public Sprite AddSprite;
    public Sprite MultSprite;

    public void SetSprite()
    {
        Sprite.sprite = Operand.isAdd ? AddSprite : MultSprite;
    }
}
