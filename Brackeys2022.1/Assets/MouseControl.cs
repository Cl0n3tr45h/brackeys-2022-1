using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerInventorySlotData))]
public class MouseControl : MonoBehaviour
{
    private Collider2D collider;
    private PlayerInventorySlotData slotData;
    private bool isHeld;

    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        slotData = GetComponent<PlayerInventorySlotData>();
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MouseSignal();
        if (isHeld)
        {
            Vector3 mousePos = Mouse.GetMousePos(0);
            this.gameObject.transform.position = mousePos;
        }
        
    }

    void MouseSignal()
    {
        bool onSlot = Mouse.MouseHover(collider);
        if (Input.GetMouseButtonDown(0) && onSlot)
        {
            isHeld = true;
            var mousePos = Mouse.GetMousePos(0);
        }
        if (Input.GetMouseButtonUp(0) && isHeld)
        {
            isHeld = false;
            if (InventoryManager.hoverControl != null) //Complex number gets saved to inventory Slot if let go over such
            {
                InventoryManager.hoverControl.ComplexNumber = slotData.ComplexNumber;
                this.gameObject.transform.SetParent(InventoryManager.hoverControl.gameObject.transform);
            }
            else
            {
                this.gameObject.transform.SetParent(DisplayInventory.FirstEmptyPanel().transform);
            }
        }
        if (onSlot)
        {
            MouseManager.EnterHover(this);
        }
    }
}
