using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using TMPro;
using System.ComponentModel;

public class CallLogic : MonoBehaviour
{
    [Header("Image and Video Data")]
    public List<CallDataScriptableObject> callsData;  // Ссылка на Scriptable Object

    [Header("UI Elements")]
    public Transform gridParent;   // Родительский объект для сетки
    public GameObject modalWindow; // Окно звонка
    public Image internalImage; // Картинка внутри
    public AudioSource internalAudio; // Звук внутри
    public GameObject callPrefab;
    public GameObject openPrefab;
    public DataScriptableObject data;

    // Метод для генерации таблицы 3x3
    void Start()
    {
        GenerateGrid();
    }

    // Генерация сетки 3x3 с изображениями
    void GenerateGrid()
    {
        for (int i = 0; i < callsData.Count; i++)
        {
            GameObject newImage;
            if (callsData[i].price <= 0)
            {
                newImage = Instantiate(callPrefab, gridParent);
                Image imgComponent = newImage.GetComponent<Image>();
                imgComponent.sprite = callsData[i].externalImage; // Используем данные из Scriptable Object
                // Добавляем обработчик события нажатия на картинку
                int index = i;  // Локальная переменная для замыкания
                newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClick(index, true));
            }
            else
            {
                newImage = Instantiate(openPrefab, gridParent);
                TextMeshProUGUI open_text = newImage.GetComponent<TextMeshProUGUI>();
                open_text.text = open_text.text.Substring(0, -1) + callsData[i].price.ToString();
                // Добавляем обработчик события нажатия на картинку
                int index = i;  // Локальная переменная для замыкания
                newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClick(index, false));
            }
        }
    }

    void OnImageClick(int index, bool isOpen)
    {
        if (isOpen)
        {
            internalImage.sprite = callsData[index].internalImage;
            internalAudio.clip = callsData[index].internalAudio;
            modalWindow.SetActive(true);
            internalAudio.Play();
        }
        else
        {
            if (data.points >= callsData[index].price)
            {
                data.points -= callsData[index].price;
                callsData[index].isOpen = true;
                imgComponent.sprite = callsData[i].externalImage; // Используем данные из Scriptable Object
            }
        }
    }
}
