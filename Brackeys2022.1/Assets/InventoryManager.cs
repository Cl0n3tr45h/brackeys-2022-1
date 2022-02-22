using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager

{
    public static InventoryController hoverControl;
    
    public static void EnterHover(InventoryController mouseControl)
    {
        hoverControl = mouseControl;
    }

    public static void ExitHover(InventoryController mouseControl)
    {
        if(hoverControl == mouseControl)
            hoverControl = null;
    }
}
