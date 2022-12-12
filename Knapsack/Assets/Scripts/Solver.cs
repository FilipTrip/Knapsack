using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Solver : MonoBehaviour
{
    [SerializeField] private int numberOfknapsacks;
    [SerializeField] private float minWeightCapacity;
    [SerializeField] private float maxWeightCapacity;
    [SerializeField] private Knapsack[] knapsacks;

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

    public void Solve1()
    {
        List<Item> sortedItems = GetComponent<ItemGenerator>().Items.OrderBy(item => -item.relativeValue).ToList();

        int knapsacksChecked = 0;
        int knapsackIndex = -1;
        Knapsack knapsack;
        Item item;

        for (int i = 0; i < sortedItems.Count; ++i)
        {
            knapsackIndex = (knapsackIndex + 1) % knapsacks.Length;
            knapsack = knapsacks[knapsackIndex];

            if (knapsacksChecked == knapsacks.Length)
            {
                ++i;
                knapsacksChecked = 0;

                if (i >= sortedItems.Count)
                    break;
            }

            item = sortedItems[i];

            if (knapsack.TotalWeight + item.weight <= knapsack.WeightCapacity)
            {
                knapsack.Add(item);
                sortedItems.RemoveAt(i);
                knapsacksChecked = 0;
            }
            else
            {
                ++knapsacksChecked;
            }

            --i;
        }

        Debug.Log(sortedItems.Count + " items left");

        Finished.Invoke();
    }

}
