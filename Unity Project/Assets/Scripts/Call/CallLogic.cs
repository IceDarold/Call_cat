using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using TMPro;
using System.ComponentModel;

public class CallLogic : MonoBehaviour
{
    [Header("Image and Video Data")]
    public List<CallDataScriptableObject> callsData;  // ������ �� Scriptable Object

    [Header("UI Elements")]
    public Transform gridParent;   // ������������ ������ ��� �����
    public GameObject modalWindow; // ���� ������
    public Image internalImage; // �������� ������
    public AudioSource internalAudio; // ���� ������
    public GameObject callPrefab;
    public GameObject openPrefab;
    public DataScriptableObject data;

    // ����� ��� ��������� ������� 3x3
    void Start()
    {
        GenerateGrid();
    }

    // ��������� ����� 3x3 � �������������
    void GenerateGrid()
    {
        for (int i = 0; i < callsData.Count; i++)
        {
            GameObject newImage;
            if (callsData[i].price <= 0)
            {
                newImage = Instantiate(callPrefab, gridParent);
                Image imgComponent = newImage.GetComponent<Image>();
                imgComponent.sprite = callsData[i].externalImage; // ���������� ������ �� Scriptable Object
                // ��������� ���������� ������� ������� �� ��������
                int index = i;  // ��������� ���������� ��� ���������
                newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClick(index, true));
            }
            else
            {
                newImage = Instantiate(openPrefab, gridParent);
                TextMeshProUGUI open_text = newImage.GetComponent<TextMeshProUGUI>();
                open_text.text = open_text.text.Substring(0, -1) + callsData[i].price.ToString();
                // ��������� ���������� ������� ������� �� ��������
                int index = i;  // ��������� ���������� ��� ���������
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
                imgComponent.sprite = callsData[i].externalImage; // ���������� ������ �� Scriptable Object
            }
        }
    }
}
