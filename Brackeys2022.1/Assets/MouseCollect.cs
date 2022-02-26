using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerInventorySlotData))]
public class MouseCollect : MonoBehaviour
{
    public GameLoop GameLoop;   
    private Collider2D collider;
    private PlayerInventorySlotData slotData;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        slotData = GetComponent<PlayerInventorySlotData>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseSignal();
    }
    void MouseSignal()
    {
        bool onSlot = Mouse.MouseHover(collider);
        if (Input.GetMouseButtonDown(0) && onSlot)
        {
            GameLoop.InventoryObject.Add(slotData.ComplexNumberObject);
        }
        if (onSlot)
        {
            MouseManager.EnterCollect(this);
        }
        else
        {
            MouseManager.ExitCollect(this);
        }
        
    }
}
