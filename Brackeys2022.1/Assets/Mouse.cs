using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public static Vector3 GetMousePos(float _depth)
    {
        
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = _depth;
        return mousePos;
    }
}
