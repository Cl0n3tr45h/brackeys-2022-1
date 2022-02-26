using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    public List<ComplexNumberObject> SelectedLoot = new List<ComplexNumberObject>();
    public ComplexOperand Operand;
    public Toggle[] Toggles;
    public Button NextLevelButton;
    
    public void OnSetActive()
    {
        SelectedLoot = new List<ComplexNumberObject>();
        Operand = new ComplexOperand();
        foreach (var toggle in Toggles)
        {
            toggle.onValueChanged.AddListener(delegate
            {
                OnValueChanged(toggle);
            });
        }
    }

    private void Update()
    {
        if (SelectedLoot.Count >= 2)
        {
            ManageRemainingToggles(true);
            ManageButton(true);
        }
        else
        {
            ManageRemainingToggles(false);
            ManageButton(false);
        }
    }

    public void ManageButton(bool _interactable)
    {
        NextLevelButton.interactable = _interactable;
    }

    public void OnValueChanged( Toggle toggle)
    {
        var _complexNumber = toggle.transform.GetComponentInParent<Loot>().ComplexNumber;
            if (toggle.isOn)
                SelectedLoot.Add(_complexNumber);
            else
                SelectedLoot.Remove(_complexNumber);
            Debug.Log(_complexNumber.ComplexNumber.Print());
    }

    public void ManageRemainingToggles(bool _lock)
    {
        foreach (var toggle in Toggles)
        {
            if (!toggle.isOn) 
                toggle.interactable = !_lock;
        }
        
    }

}
