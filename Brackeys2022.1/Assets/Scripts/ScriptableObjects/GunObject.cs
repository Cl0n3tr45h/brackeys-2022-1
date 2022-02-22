using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun(Complex)", menuName = "ScriptableObjects/Gun")]
public class GunObject : ScriptableObject
{
    public List<ComplexNumberData> Damage = new List<ComplexNumberData>();
    public List<ComplexNumberData> Range = new List<ComplexNumberData>();
    public List<ComplexNumberData> ProjectileSpeed = new List<ComplexNumberData>();
    public List<ComplexNumberData> MagSize = new List<ComplexNumberData>();
    public List<ComplexNumberData> FireRate = new List<ComplexNumberData>();

}
