using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    public List<ComplexNumberObject> SelectedLoot = new List<ComplexNumberObject>();
    public Toggle[] Toggles;
    
    public void OnSetActive()
    {
        
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
        }
        else
        {
            ManageRemainingToggles(false);
        }
    }
    // AT END
    // übergeben Inventory => neue Werte
    // ==> LIST
    //LIST erstellt bei OnValueChanged

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
