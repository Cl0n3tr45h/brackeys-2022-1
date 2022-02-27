using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public InventoryController ComplexStatPanel;
    public PlayerInventorySlotData PlayerInventorySlot;

    public void FillStatPanel(InventoryController _panel)
    {
        Debug.Log("Pressed!" + _panel.gameObject.name);
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
            else if (PlayerInventorySlot != null )
            {
                if(!PlayerInventorySlot.isUsed)
                {
                    ComplexStatPanel.ComplexNumber = PlayerInventorySlot.ComplexNumber;
                    PlayerInventorySlot.isUsed = true;
                    ComplexStatPanel.Print();
                    PlayerInventorySlot.Hide();
                }
                else
                {
                    
                    ComplexStatPanel = _panel;
                }
            }
        }
    }

    public void Subscribe(PlayerInventorySlotData _data)
    {
        _data.CraftButton.onClick.AddListener(delegate { GetInventorySlot(_data); });
    }
    
    public void GetInventorySlot(PlayerInventorySlotData _slot)
    {
        
        Debug.Log("Pressed Complex Number!" + _slot.gameObject.name);
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
