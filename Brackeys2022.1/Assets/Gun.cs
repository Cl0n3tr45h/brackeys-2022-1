using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public PlayerMovement Player;
    public ComplexNumberData Damage;
    private int currentDamage;
    public ComplexNumberData ProjectileSpeed;
    private int currentProjectileSpeed;

    public ComplexNumberData FireRate;
    private int currentFireRate;
    private float shotIntervalTimer;

    public ComplexNumberData Range;
    private int currentRange;

    public ComplexNumberData MagSize;
    public int currentMagSize;
    
    public GameObject BulletPrefab;
    public Transform BulletSpawn;

    public LayerMask WhatIsEnemy;
    
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        currentDamage = Damage.real;
        currentProjectileSpeed = ProjectileSpeed.real;
        currentFireRate = FireRate.real;
        shotIntervalTimer = 0;
        currentRange = Range.real;
        currentMagSize = MagSize.real;
    }

    // Update is called once per frame
    void Update()
    {
        Turning();
        CheckRotation();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if(shotIntervalTimer > 0)
            shotIntervalTimer -= Time.deltaTime;
    }
    
    private void Turning()
    {
        mousePos = Mouse.GetMousePos(0);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        /*
         //want to do some mirroring shit here but aaaaaah
         if (transform.position.x - mousePos.x > 0 && facingRight)
         {
             facingRight = false;
             transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
         }*/
        
        transform.right = direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(BulletSpawn.position, -(transform.position-Mouse.GetMousePos(0)).normalized * currentRange);
    }

    void Shoot()
    {
        if (shotIntervalTimer <= 0 && currentMagSize > 0)
        {
            //do the shooting
            shotIntervalTimer = 1/(currentFireRate*2);
            //currentMagSize--;
           /* GameObject Bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);
            //Bullet.transform.right = mousePos;
            Bullet.GetComponent<bulletMovement>().ProjectileSpeed = currentProjectileSpeed;
            Bullet.GetComponent<bulletMovement>().LifeTime = currentRange;
            Debug.Log("I am doing " + currentDamage + " Damage owo");
            */
           RaycastHit2D hit = Physics2D.Raycast(BulletSpawn.position, -(transform.position-Mouse.GetMousePos(0)).normalized, currentRange,
               WhatIsEnemy, Mathf.NegativeInfinity, Mathf.Infinity);
           if (hit.collider != null)
           {
               Debug.Log("hit!");
               hit.collider.gameObject.GetComponentInParent<Enemy>().Damage(currentDamage);
           }
           else
           {
               Debug.Log("no hit!");
           }
        }
        else
        {
            //Player needs to know gun is on cooldown??!!
            //maybe???
        }
        
    }

    void CheckRotation()
    {
        if (!Player.m_FacingRight && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }

        if (Player.m_FacingRight && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }
        
    }

    public void OnShift()
    {
        if (currentDamage == Damage.real)
        {
            currentDamage = Damage.imaginary;
            currentProjectileSpeed = ProjectileSpeed.imaginary;
            currentFireRate = FireRate.imaginary;
            currentRange = Range.imaginary;
            currentMagSize = MagSize.imaginary;
        }
        else
        {
            currentDamage = Damage.real;
            currentFireRate = FireRate.real;
            currentProjectileSpeed = ProjectileSpeed.real;
            currentRange = Range.real;
            currentMagSize = MagSize.real;
        }
        
        shotIntervalTimer = 0;
    }
}