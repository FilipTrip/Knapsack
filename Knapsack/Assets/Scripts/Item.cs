using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    public float weight;
    public float value;

    public Item(float weight, float value)
    {
        this.weight = weight;
        this.value = value;
    }

    public override string ToString()
    {
        return "w: " + weight.ToString("0.00") + " v: " + value.ToString("0.00");
    }
}
