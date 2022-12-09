using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGenerator : MonoBehaviour
{
    [Header("Values")]

    [SerializeField] private int itemCount;
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
        items = new Item[itemCount];

        for (int i = 0; i < itemCount; ++i)
        {
            items[i].weight = weightDistribution.Evaluate(Random.Range(0f, 1f));
            items[i].value  = valueDistribution .Evaluate(Random.Range(0f, 1f));
        }

        Finished.Invoke();
    }
}
