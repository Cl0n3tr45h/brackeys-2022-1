using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Complex Number", menuName = "ScriptableObjects/Complex Number")]
public class ComplexNumberObject : ScriptableObject
{
    public ComplexNumberData ComplexNumber;

    public string Print()
    {
        string text = ComplexNumber.real + " + " + ComplexNumber.imaginary + "i";
        return text;
    }

    private void OnEnable()
    {
        if(ComplexNumber.real == 0 || ComplexNumber.imaginary == 0)
        {
            ComplexNumber.real = Random.Range(3, 8);

            ComplexNumber.imaginary = Random.Range(3, 8);

            //10% chance of the Number object to have one component be 1 while the other doubles in value
            if (Random.Range(1, 10) >= 10)
            {
                if (Random.Range(1, 2) == 1)
                {
                    ComplexNumber.real = Mathf.Max(ComplexNumber.real * 2, 12);
                    ComplexNumber.imaginary = 1;
                }
                else
                {

                    ComplexNumber.real = 1;
                    ComplexNumber.imaginary = Mathf.Max(ComplexNumber.imaginary * 2, 12);;
                }
            }
        }
    }
}

[System.Serializable]
public struct ComplexNumberData
{
    public int real;
    public int imaginary;

    public ComplexNumberData(int _real, int _imaginary)
    {
        real = _real;
        imaginary = _imaginary;
    }
    public string Print()
    {
        string text = real + " + " + imaginary + "i";
        return text;
    }

    public string RichPrint()
    {
        string text = "<color=#d61517>" + real + "</color><color=white>+</color><color=#15D68D>" + imaginary + "i</color>";
        return text;
    }


    
    public static ComplexNumberData Add(ComplexNumberData _summand1, ComplexNumberData _summand2)
    {
        int resultReal = _summand1.real + _summand2.real;
        int resultImaginary = _summand1.imaginary + _summand2.imaginary;
       
        return new ComplexNumberData(resultReal, resultImaginary);
    }

    public static ComplexNumberData Add(List<ComplexNumberData> _summands)
    {
        int resultReal = 0;
        int resultImaginary = 0;
        foreach (var number in _summands)
        {
            resultReal += number.real;
            resultImaginary += number.imaginary;
        }
        return new ComplexNumberData(resultReal, resultImaginary);
    }

    public static ComplexNumberData Multiply(ComplexNumberData _factor1, ComplexNumberData _factor2)
    {
        return new ComplexNumberData(_factor1.real * _factor2.real - _factor1.imaginary * _factor2.imaginary,
            _factor1.real * _factor2.imaginary + _factor1.imaginary * _factor2.real);
    }

    public static ComplexNumberData Multiply(ComplexNumberData _factor1, ComplexNumberData _factor2,
        ComplexNumberData _factor3)
    {
        var firstProduct = ComplexNumberData.Multiply(_factor1, _factor2);
        var result = ComplexNumberData.Multiply(firstProduct, _factor3);
        return result;
    }
}

[System.Serializable]
public struct ComplexOperand
{
    public bool isAdd;

    public string Print()
    {
        if (isAdd)
        {
            return "+";
        }
        else
        {
            return "*";
        }
    }
}