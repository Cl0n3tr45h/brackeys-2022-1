using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    private Gun Gun;

    public GameObject BulletPrefab;

    public Transform BulletSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Gun = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }
    }

    
}
