using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public ComplexNumberObject ComplexNumber;
    
    public TextMeshProUGUI Text;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print()
    {
        Text.text = ComplexNumber.ComplexNumber.RichPrint();
    }
}
