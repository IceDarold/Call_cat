using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CallUI : MonoBehaviour
{
    [SerializeField] GameObject CallWindow;
    [SerializeField] GameObject MainWindow;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] DataScriptableObject data;
    [SerializeField] GameObject calls_visual;
    private int save_points = 0;
    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        if (save_points != data.points)
        {
            save_points = data.points;
            int lastIndex = points.text.LastIndexOf('\n');
            points.text = points.text.Substring(0, lastIndex + 1) + data.points.ToString();
        }
    }
    public void UpdateUI()
    {
        calls_visual.SetActive(true);
    }

    public void Reset()
    {
        CallWindow.SetActive(false);
        MainWindow.SetActive(true);
    }
    public void Finish()
    {
        calls_visual.SetActive(false);
    }
}
