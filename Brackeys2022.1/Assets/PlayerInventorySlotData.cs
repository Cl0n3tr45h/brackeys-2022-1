using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventorySlotData : MonoBehaviour
{
    public ComplexNumberObject ComplexNumberObject;

    private void Start()
    {
        ComplexNumberObject = ScriptableObject.Instantiate(ComplexNumberObject);
    }
}
