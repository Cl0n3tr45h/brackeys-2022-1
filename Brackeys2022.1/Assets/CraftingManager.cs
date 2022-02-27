using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public InventoryController ComplexStatPanel;
    public PlayerInventorySlotData PlayerInventorySlot;

    public void FillStatPanel(InventoryController _panel)
    {
        //no panel -> simple Zuweisung
        if (!ComplexStatPanel)
        {
            ComplexStatPanel = _panel;
        }
        else
        {
            //same panel clicked -> deselektion
            if (ComplexStatPanel == _panel)
            {
                ComplexStatPanel = null;
            }
            //panel gefüllt und Inventory slot ausgeählt -> wert Zuweisung
            else if (PlayerInventorySlot != null && !PlayerInventorySlot.isUsed)
            {
                ComplexStatPanel.ComplexNumber = PlayerInventorySlot.ComplexNumber;
                PlayerInventorySlot.isUsed = true;
                ComplexStatPanel.Print();
                PlayerInventorySlot.Hide();
            }
            else if(PlayerInventorySlot.isUsed)
            {
                ComplexStatPanel = _panel;
            }
        }
    }

    public void GetInventorySlot(PlayerInventorySlotData _slot)
    {
        if (_slot.isUsed)
            return;
        if (!PlayerInventorySlot)
        {
            PlayerInventorySlot = _slot;
        }
        else
        {
            if (PlayerInventorySlot == _slot)
            {
                PlayerInventorySlot = null;
            }
            else
            {
                
                PlayerInventorySlot = _slot;
            }
        }
    }
    
 
}
