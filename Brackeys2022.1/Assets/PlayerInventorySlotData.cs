using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySlotData : MonoBehaviour
{
    public ComplexNumberObject ComplexNumber;
    public bool isUsed;

    public Button CraftButton;
    public CraftingManager CraftingManager;
    private void Start()
    {
        //ComplexNumber = ScriptableObject.Instantiate(ComplexNumber);
    }

    public void OnSetActive()
    {
        CraftingManager.Subscribe(this);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
