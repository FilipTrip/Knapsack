using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Knapsack
{
    private List<Item> items;
    private float weightCapacity;
    private float totalValue;
    private float totalWeight;

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
        return true;
    }

    public bool Remove(Item item)
    {
        if (items.Remove(item) == false)
            return false;

        totalWeight -= item.weight;
        totalValue -= item.value;
        return true;
    }

    public void RemoveAt(int i)
    {
        Item item = items[i];
        totalValue -= item.value;
        totalWeight -= totalWeight;
        items.RemoveAt(i);
    }

}
