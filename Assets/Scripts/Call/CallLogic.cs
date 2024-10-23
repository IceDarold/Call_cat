using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

public class CallLogic : MonoBehaviour
{
    [Header("Image and Video Data")]
    public List<CallDataScriptableObject> callsData;  // Ссылка на Scriptable Object

    [Header("UI Elements")]
    public Transform gridParent;   // Родительский объект для сетки
    public GameObject modalWindow; // Окно звонка
    public Image internalImage; // Картинка внутри
    public AudioSource internalAudio; // Картинка внутри
    public GameObject callPrefab;

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
            GameObject newImage = Instantiate(callPrefab, gridParent);
            Image imgComponent = newImage.GetComponent<Image>();
            imgComponent.sprite = callsData[i].externalImage; // Используем данные из Scriptable Object

            // Добавляем обработчик события нажатия на картинку
            int index = i;  // Локальная переменная для замыкания
            newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }
    }

    void OnImageClick(int index)
    {
        internalImage.sprite = callsData[index].internalImage;
        internalAudio.clip = callsData[index].internalAudio;
        modalWindow.SetActive(true); // Показываем модальное окно
        internalAudio.Play();
    }
}
