using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public int ProjectileSpeed;

    public int LifeTime;

    private float currentLifeTime;

    private Vector3 direction;
    // Start is called before the first frame update
    void OnEnable()
    {
        direction = - (transform.position - Mouse.GetMousePos(0)).normalized;
        Debug.Log(direction.x +" " + direction.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * ProjectileSpeed * Time.deltaTime);
        if (currentLifeTime >= LifeTime)
        {
            Destroy(this.gameObject);
        }

        currentLifeTime += Time.deltaTime * ProjectileSpeed;
    }
}
