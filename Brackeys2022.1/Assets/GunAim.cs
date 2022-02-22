using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    private Vector3 mousePos;
    private bool facingRight = true;

    // Update is called once per frame
    void Update()
    {
        {

            mousePos = Mouse.GetMousePos(0);
            Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

            transform.right = direction;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, mousePos);
    }

    
}
