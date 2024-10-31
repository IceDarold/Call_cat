using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Counter : MonoBehaviour
{

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        DataController.OnPointsCountChanged.AddListener(UpdateText);
        
    }

    private void Start()
    {
        UpdateText(DataController.LoadData());
    }

    private void OnDestroy()
    {
        DataController.OnPointsCountChanged.RemoveListener(UpdateText);
    }

    private void UpdateText(int points)
    {
        text.text = "Бушки " + points.ToString();
    }
}
