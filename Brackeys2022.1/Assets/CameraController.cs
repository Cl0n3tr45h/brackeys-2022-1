using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;

    public float CameraWiggle;

    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var centerX = Player.transform.position.x;
        var centerY = Player.transform.position.y;
        var mousePos = Mouse.GetMousePos(0);
        var deltaX = centerX - mousePos.x;
        var deltaY = centerY - mousePos.y;
        deltaX *= CameraWiggle;
        deltaY *= CameraWiggle;

        var finalX =Mathf.Clamp(centerX - deltaX, -10, 10);
        var finalY = Mathf.Clamp(centerY - deltaY, -8, 8);
        this.transform.position = origin + new Vector3(finalX, finalY);
        
    }
/*
    private bool CheckMouseToBounds()
    {
        var width = Screen.width;
        var height = Screen.height;
        var mousePos = Mouse.GetMousePos(0);
        
        if(mousePos.x > 0 + WidthThreshold && mousePos.x < width - WidthThreshold
            && )
        
        return false;
    }*/
}
