using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventorySlotData : MonoBehaviour
{
    public ComplexNumberObject ComplexNumber;
    public bool isUsed;

    private void Start()
    {
        //ComplexNumber = ScriptableObject.Instantiate(ComplexNumber);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
