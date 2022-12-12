using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Solver : MonoBehaviour
{
    [Header("Knapsacks")]
    [SerializeField] private int numberOfknapsacks;
    [SerializeField] private float minWeightCapacity;
    [SerializeField] private float maxWeightCapacity;
    [SerializeField] private Knapsack[] knapsacks;

    private List<Item> items;
    public UnityEvent Finished = new UnityEvent();

    public Knapsack[] Knapsacks => knapsacks;

    public void CreateKnapsacks()
    {
        knapsacks = new Knapsack[numberOfknapsacks];

        float weightCapacity;
        for (int i = 0; i < numberOfknapsacks; ++i)
        {
            weightCapacity = minWeightCapacity + (maxWeightCapacity - minWeightCapacity) * Random.Range(0f, 1f);
            knapsacks[i] = new Knapsack(weightCapacity);
        }
    }

    public void Greedy()
    {
        items = GetComponent<ItemGenerator>().Items.OrderBy(item => -item.relativeValue).ToList();

        int knapsacksChecked = 0;
        int knapsackIndex = -1;
        Knapsack knapsack;
        Item item;

        for (int i = 0; i < items.Count; ++i)
        {
            knapsackIndex = (knapsackIndex + 1) % knapsacks.Length;
            knapsack = knapsacks[knapsackIndex];

            if (knapsacksChecked == knapsacks.Length)
            {
                ++i;
                knapsacksChecked = 0;

                if (i >= items.Count)
                    break;
            }

            item = items[i];

            if (knapsack.TotalWeight + item.weight <= knapsack.WeightCapacity)
            {
                knapsack.Add(item);
                items.RemoveAt(i);
                knapsacksChecked = 0;
            }
            else
            {
                ++knapsacksChecked;
            }

            --i;
        }

        float totalValue = 0;
        foreach (Knapsack knapsack1 in knapsacks)
            totalValue += knapsack1.TotalValue;

        Debug.Log(items.Count + " items left");
        Debug.Log("Total value: " + totalValue);

        Finished.Invoke();
    }

    public void NeighborhoodSearch()
    {
        Knapsack knapsack;
        float result = float.MinValue;

        while (true)
        {
            for (int k = 0; k < knapsacks.Length; ++k)
            {
                knapsack = knapsacks[k];

                items.Add(knapsack[knapsack.Count - 1]);
                knapsack.RemoveAt(knapsack.Count - 1);

                while (true)
                {
                    List<Item> itemsThatFit = new List<Item>();

                    foreach (Item item in items)
                    {
                        if (knapsack.TotalWeight + item.weight <= knapsack.WeightCapacity)
                            itemsThatFit.Add(item);
                    }

                    if (itemsThatFit.Count == 0)
                        break;

                    itemsThatFit = itemsThatFit.OrderBy(item => -item.value).ToList();

                    knapsack.Add(itemsThatFit[0]);
                    items.Remove(itemsThatFit[0]);
                }
            }

            float totalValue = 0;
            foreach (Knapsack knapsack1 in knapsacks)
                totalValue += knapsack1.TotalValue;

            Debug.Log(items.Count + " items left");
            Debug.Log("Total value: " + totalValue);

            if (totalValue > result)
                result = totalValue;
            else
                break;
        }
    }

    public void NeighborhoodSearchSwap()
    {
        Knapsack knapsack;
        Item item;

        float result = float.MinValue;

        while (true)
        {
            for (int k = 0; k < knapsacks.Length; ++k)
            {
                knapsack = knapsacks[k];

                for (int i = 0; i < knapsack.Count; ++i)
                {
                    item = knapsack[i];

                    foreach (Knapsack other in knapsacks)
                    {
                        if (other != knapsack && other.TotalWeight + item.weight <= other.WeightCapacity)
                        {
                            knapsack.RemoveAt(i);
                            other.Add(item);
                            break;
                        }
                            
                    }
                }

                while (true)
                {
                    List<Item> itemsThatFit = new List<Item>();

                    for (int i = 0; i < items.Count; ++i)
                    {
                        item = items[i];

                        if (knapsack.TotalWeight + item.weight <= knapsack.WeightCapacity)
                            itemsThatFit.Add(item);
                    }

                    if (itemsThatFit.Count == 0)
                        break;

                    itemsThatFit = itemsThatFit.OrderBy(item => -item.relativeValue).ToList();

                    knapsack.Add(itemsThatFit[0]);
                    items.Remove(itemsThatFit[0]);
                }
            }

            float totalValue = 0;
            foreach (Knapsack knapsack1 in knapsacks)
                totalValue += knapsack1.TotalValue;

            Debug.Log(items.Count + " items left");
            Debug.Log("Total value: " + totalValue);

            if (totalValue > result)
                result = totalValue;
            else
                break;
        }
    }

}
