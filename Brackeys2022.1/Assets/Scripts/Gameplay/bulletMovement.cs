using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public int ProjectileSpeed;

    public int LifeTime;

    public int Damage;

    private float currentLifeTime;

    public bool IsReal;

    public Vector3 Direction;

    private SpriteRenderer Sprite;
    
    private bool plane;

    public LayerMask WhatIsGround;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentLifeTime = 0;
        Sprite = GetComponentInChildren<SpriteRenderer>();
        GameLoop.AddBullet(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsReal == PlaneShift.InReal)
        {
            Shift(plane = true);
            transform.Translate(Direction * ProjectileSpeed * Time.deltaTime);
            if (currentLifeTime >= LifeTime)
            {
                Destroy(this.gameObject);
            }

            currentLifeTime += Time.deltaTime;
        }
        else
        {
            Shift(plane = false);
        }
    }

    public void Shift(bool plane)
    {
        //if we're in the appropriate plane
        if (plane)
        {
            Sprite.color = Color.white;
        }
        else
        {
            Sprite.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && plane)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamageReal(Damage);
            GameLoop.RemoveBullet(this);
            Destroy(this.gameObject);
        }

        if (other.gameObject.layer == WhatIsGround)
        {
            GameLoop.RemoveBullet(this);
            Destroy(this);
        }
    }

}
