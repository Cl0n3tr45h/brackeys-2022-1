using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    public List<ComplexNumberObject> SelectedLoot = new List<ComplexNumberObject>();

    public Toggle[] Toggles;
    // Start is called before the first frame update
    public void OnSetActive()
    {
        
        foreach (var toggle in Toggles)
        {
            var complexNumber = toggle.transform.GetComponentInParent<Loot>().ComplexNumber;
            toggle.onValueChanged.AddListener(delegate
            {
                OnValueChanged(complexNumber, toggle);
            });
        }
    }

    // AT END
    // übergeben Inventory => neue Werte
    // ==> LIST
    //LIST erstellt bei OnValueChanged

    public void OnValueChanged(ComplexNumberObject _complexNumber, Toggle toggle)
    {
        if(toggle.isOn)
            SelectedLoot.Add(_complexNumber);
        else
            SelectedLoot.Remove(_complexNumber);
        
        Debug.Log(SelectedLoot.Count);
        
    }
}
