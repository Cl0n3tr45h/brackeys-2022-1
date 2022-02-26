using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerInventory", menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ComplexNumberObject> ComplexNumbers;
    public List<ComplexOperand> ComplexOperands;

    public void Add(ComplexNumberObject _object)
    {
        ComplexNumbers.Add(_object);
    }

    public void AddNumbers(List<ComplexNumberObject> _objcets)
    {
        foreach (var numberObject in _objcets)
        {
            ComplexNumbers.Add(numberObject);
        }
    }

    public string Print()
    {
        var result = ComplexNumbers.Count.ToString();
        foreach (var number in ComplexNumbers)
        {
            result += " " + number.Print();
        }

        result += ComplexOperands[0];
        return result;
    }
    public void AddOperand(ComplexOperand _object)
    {
        ComplexOperands.Add(_object);
    }
}
