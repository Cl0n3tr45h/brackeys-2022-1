using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ComplexNumberData CoNum1;
    public ComplexNumberData CoNum2;

    public List<ComplexNumberData> Numbers;
    // Start is called before the first frame update
    void Start()
    {
        ComplexNumberData resultAdd = ComplexNumberData.Add(CoNum1, CoNum2);
        Debug.Log(resultAdd.Print());
        ComplexNumberData resultMult = ComplexNumberData.Multiply(CoNum1, CoNum2);
        Debug.Log(resultMult.Print());
        ComplexNumberData resultAddList = ComplexNumberData.Add(Numbers);
        Debug.Log(resultAddList.Print());
        //ComplexNumberData resultMultList = ComplexNumberData.Multiply(Numbers);
        //Debug.Log(resultMultList.Print());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}