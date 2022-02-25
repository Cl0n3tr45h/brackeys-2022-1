using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;

    public float CameraWiggle;

    public float WidthThreshold;
    public float heightThreshold;
    public float Speed;
    private Vector3 origin;
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

            var finalX = Mathf.Clamp(centerX - deltaX, -10, 10);
            var finalY = Mathf.Clamp(centerY - deltaY, -8, 8);
            this.transform.position =new Vector3( Mathf.Lerp(origin.x, origin.x + finalX, Speed), Mathf.Lerp(origin.y, origin.y + finalY, Speed), -10);
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
