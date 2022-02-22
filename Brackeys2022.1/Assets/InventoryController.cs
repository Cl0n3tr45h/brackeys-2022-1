using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Collider2D collider;
    public ComplexNumberObject ComplexNumber;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
     
        bool onSlot = Mouse.MouseHover(collider);   
        if (onSlot)
        {
            InventoryManager.EnterHover(this);
        }
        else
        {
            InventoryManager.ExitHover(this);
        }
    }
}
