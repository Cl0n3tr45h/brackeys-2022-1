using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComplexNumber : MonoBehaviour
{
    [SerializeField] private ComplexNumberObject complexNumber;
    [SerializeField] private TextMeshPro text;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = complexNumber.Print();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            text.text = complexNumber.Print();
        }
    }
}
