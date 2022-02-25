using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCollider : MonoBehaviour
{
    public bool IsFree = true;
    public Collider2D Collider;
    public LayerMask WhatIsOccupier;
    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVacancy();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsFree ? Color.green : Color.red;

        Gizmos.DrawWireSphere(this.transform.position, 1f);
    }

    public void CheckVacancy()
    {
        if (Physics2D.OverlapCircle(this.transform.position, 1f, WhatIsOccupier, Mathf.NegativeInfinity,
            Mathf.Infinity))
        {
            IsFree = false;
        }
        else
        {
            IsFree = true;
        }
    }
}
