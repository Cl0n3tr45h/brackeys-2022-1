using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y)));
    }
}
