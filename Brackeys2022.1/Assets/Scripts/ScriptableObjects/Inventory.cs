using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerInventory", menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ComplexNumberObject> ComplexNumbers;

    public void Add(ComplexNumberObject _object)
    {
        ComplexNumbers.Add(_object);
    }
}
