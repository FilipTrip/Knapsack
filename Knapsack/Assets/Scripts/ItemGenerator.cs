using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGenerator : MonoBehaviour
{
    [Header("Values")]

    [SerializeField] private int seed;
    [SerializeField] private int itemCount;
    [SerializeField] private float minWeight;
    [SerializeField] private float minValue;
    [SerializeField] private float maxWeight;
    [SerializeField] private float maxValue;
    [SerializeField] private AnimationCurve weightDistribution;
    [SerializeField] private AnimationCurve valueDistribution;

    [Header("Result")]
    [SerializeField] private Item[] items;

    public UnityEvent Finished = new UnityEvent();

    public Item[] Items => items;

    public void Generate()
    {
        int newSeed = seed != 0 ? seed : (int)System.DateTime.Now.Ticks;
        Random.InitState(newSeed);

        items = new Item[itemCount];

        float value, weight;

        for (int i = 0; i < itemCount; ++i)
        {
            weight = minWeight + (maxWeight - minWeight) * weightDistribution.Evaluate(Random.Range(0f, 1f));
            value  = minValue  + (maxValue  - minValue)  * valueDistribution .Evaluate(Random.Range(0f, 1f));
            items[i] = new Item(weight, value);
        }

        Finished.Invoke();
    }
}
