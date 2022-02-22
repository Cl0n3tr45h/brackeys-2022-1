using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Complex Number", menuName = "ScriptableObjects/Complex Number")]
public class ComplexNumberObject : ScriptableObject
{
    public ComplexNumberData ComplexNumber;

    public string Print()
    {
        string text = ComplexNumber.real + " + " + ComplexNumber.imaginary + "i";
        return text;
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

    public static ComplexNumberData Multiply(List<ComplexNumberData> _factors)
    {
        
        ComplexNumberData result = new ComplexNumberData();
        if (_factors.Count > 2)
        {
            //multiply mit funktion
            var _shortenedFactors = new List<ComplexNumberData>(_factors);
                _shortenedFactors.RemoveAt(_shortenedFactors.Count-1);
            result = Add(result, Multiply(_shortenedFactors));
        }
        else
        {
            //multiply mit formel
            result = Add(result, Multiply(_factors[0] ,_factors[1]));
        }
        return result;
    }
}