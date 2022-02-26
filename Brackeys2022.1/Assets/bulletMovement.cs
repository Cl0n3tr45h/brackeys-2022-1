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
    

    public Vector3 Direction;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentLifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * ProjectileSpeed * Time.deltaTime);
        if (currentLifeTime >= LifeTime)
        {
            Destroy(this.gameObject);
        }

        currentLifeTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamageReal(Damage);
            Destroy(this.gameObject);
        }
    }
}
