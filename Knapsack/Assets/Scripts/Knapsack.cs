using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Knapsack
{
    [SerializeField] private List<Item> items;
    [SerializeField] private float weightCapacity;
    [SerializeField] private float totalValue;
    [SerializeField] private float totalWeight;
    [SerializeField] private float filledPercentage;

    public float WeightCapacity => weightCapacity;
    public float TotalWeight => totalWeight;
    public float TotalValue => totalValue;
    public int Count => items.Count;

    public Knapsack(float weightCapacity)
    {
        this.weightCapacity = weightCapacity;
        items = new List<Item>();
    }

    public bool Add(Item item)
    {
        if (totalWeight + item.weight > weightCapacity)
            return false;

        items.Add(item);
        totalWeight += item.weight;
        totalValue += item.value;
        filledPercentage = totalWeight / weightCapacity;
        return true;
    }

    public bool Remove(Item item)
    {
        if (items.Remove(item) == false)
            return false;

        totalWeight -= item.weight;
        totalValue -= item.value;
        filledPercentage = totalWeight / weightCapacity;
        return true;
    }

    public void RemoveAt(int i)
    {
        Item item = items[i];
        totalValue -= item.value;
        totalWeight -= totalWeight;
        filledPercentage = totalWeight / weightCapacity;
        items.RemoveAt(i);
    }

    public new string ToString()
    {
        return "c: " + weightCapacity.ToString("0.00") + " \t%: " + (filledPercentage * 100f).ToString("0.0");
    }

}
