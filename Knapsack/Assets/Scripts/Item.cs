using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    public float weight;
    public float value;
    public float relativeValue;

    public Item(float weight, float value)
    {
        this.weight = weight;
        this.value = value;
        relativeValue = value / weight;
    }

    public override string ToString()
    {
        return "w: " + weight.ToString("0.00") + " v: " + value.ToString("0.00") + " v/w: " + relativeValue.ToString("0.00");
    }
}
