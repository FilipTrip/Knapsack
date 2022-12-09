using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private RectTransform itemScrollViewContent;
    [SerializeField] private RectTransform knapsackScrollViewContent;

    private void Awake()
    {
        UpdateLog();
        GameObject.Find("Game Manager").GetComponent<ItemGenerator>().Finished.AddListener(UpdateLog);
    }

    private void ClearLog()
    {
        foreach (Transform child in itemScrollViewContent)
            Destroy(child.gameObject);

        foreach (Transform child in knapsackScrollViewContent)
            Destroy(child.gameObject);
    }

    private void UpdateLog()
    {
        ClearLog();
        Item[] items = GameObject.Find("Game Manager").GetComponent<ItemGenerator>().Items;

        for (int i = 0; i < items.Length; ++i)
        {
            TextMeshProUGUI text = Instantiate(linePrefab, itemScrollViewContent).GetComponent<TextMeshProUGUI>();
            text.text = i + "\t " + items[i].ToString();
        }

        /*Knapsack[] knapsacks = GameObject.Find("Game Manager").GetComponent<KnapsackGenerator>().Knapsacks;

        for (int i = 0; i < knapsacks.Length; ++i)
        {
            TextMeshProUGUI text = Instantiate(linePrefab, knapsackScrollViewContent).GetComponent<TextMeshProUGUI>();
            text.text = i + "\t " + knapsacks[i].ToString();
        }*/

        itemScrollViewContent.sizeDelta = new Vector2(itemScrollViewContent.sizeDelta.x, itemScrollViewContent.childCount * linePrefab.GetComponent<RectTransform>().sizeDelta.y + 1);
        knapsackScrollViewContent.sizeDelta = new Vector2(knapsackScrollViewContent.sizeDelta.x, knapsackScrollViewContent.childCount * linePrefab.GetComponent<RectTransform>().sizeDelta.y + 1);

    }

}
