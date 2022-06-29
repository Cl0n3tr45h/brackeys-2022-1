using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager //manages where the mouse is in relation to inventory things for that good good Drag and Drop
{
    public static MouseControl holdControl;
    public static MouseControl hoverControl;
    public static MouseCollect collectControl;
    
    public static void StartHold(MouseControl nodeControl)
    {
        holdControl = nodeControl;
    }

    public static void StopHold(MouseControl nodeControl)
    {
        if(holdControl == nodeControl)
            holdControl = null;
    }
    
    public static void EnterHover(MouseControl mouseControl)
    {
        hoverControl = mouseControl;
    }

    public static void ExitHover(MouseControl mouseControl)
    {
        if(hoverControl == mouseControl)
            hoverControl = null;
    }
    public static void EnterCollect(MouseCollect mouseControl)
    {
        collectControl = mouseControl;
    }

    public static void ExitCollect(MouseCollect mouseControl)
    {
        if(collectControl == mouseControl)
            collectControl = null;
    }
}
