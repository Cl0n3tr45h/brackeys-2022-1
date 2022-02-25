using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;

    public float CameraWiggle;

    private float WidthThreshold;
    private float heightThreshold;
    public float Speed;
    private Vector3 origin;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        origin = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (CheckMouseToBounds())
        //{
            var centerX = Player.transform.position.x;
            var centerY = Player.transform.position.y;
            var mousePos = Mouse.GetMousePos(0);
            var deltaX = centerX - mousePos.x;
            var deltaY = centerY - mousePos.y;
            deltaX *= CameraWiggle;
            deltaY *= CameraWiggle;

            
            WidthThreshold = Screen.width;
            heightThreshold = Screen.height;
            var finalX = Mathf.Clamp(centerX - deltaX, centerX - WidthThreshold, centerX + WidthThreshold);
            var finalY = Mathf.Clamp(centerY - deltaY, centerY - heightThreshold, centerY + heightThreshold);

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(finalX, finalY, -10), ref velocity, 0.3f);
            //this.transform.position =new Vector3( Mathf.Lerp(origin.x, origin.x + finalX, Speed), Mathf.Lerp(origin.y, origin.y + finalY, Speed), -10);
            //}
    }

    /*
    private bool CheckMouseToBounds()
    {
        var width = Screen.width;
        var height = Screen.height;
        var mousePos = Mouse.GetMousePos(0);

        return mousePos.x > 0 + WidthThreshold && mousePos.x < width - WidthThreshold
                                               && mousePos.y > 0 + heightThreshold &&
                                               mousePos.x < height - heightThreshold;
    }*/
}
