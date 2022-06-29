using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ComplexNumberData CoNum1;
    public ComplexNumberData CoNum2;

    public List<ComplexNumberData> Numbers;

    public PlayerHealth ph;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
           // ph.TakeDamageReal(5);
        }   
    }
}