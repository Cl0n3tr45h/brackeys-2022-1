using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameLoop GameLoop;
    public PlayerInventorySlotData data;
    public ComplexNumberObject ComplexNumber;

    public TextMeshProUGUI ValueText;
    // Start is called before the first frame update
    void Start()
    {
        ComplexNumber = new ComplexNumberObject();
        ValueText.text = ComplexNumber.ComplexNumber.real + "+" + ComplexNumber.ComplexNumber.imaginary + "i";
    }

    public void AddToInventory()
    {
        GameLoop.InventoryObject.Add(ComplexNumber);
    }
}
