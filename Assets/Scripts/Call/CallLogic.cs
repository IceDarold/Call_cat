using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using TMPro;
using System.ComponentModel;

public class CallLogic : MonoBehaviour
{
    [Header("Image and Video Data")]
    public List<CallDataScriptableObject> callsData;

    [Header("UI Elements")]
    public Transform gridParent;   
    public GameObject modalWindow; 
    public Image internalImage;
    public AudioSource internalAudio;
    public GameObject callPrefab;
    public DataScriptableObject data;
    public Sprite lock_image;
    public TextMeshProUGUI priceTextPrefab;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < callsData.Count; i++)
        {
            GameObject newImage = Instantiate(callPrefab, gridParent);
            Image imgComponent = newImage.GetComponent<Image>();
            if (callsData[i].price <= 0 || callsData[i].isOpen)
            {
                imgComponent.sprite = callsData[i].externalImage;
                newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClick(i, true));
            }
            else
            {
                Debug.Log("Instantiate" + i.ToString());
                imgComponent.sprite = lock_image;
                //Turn off call icon
                newImage.GetComponentsInChildren<Image>()[1].gameObject.SetActive(false);
                //Turn on text

                TextMeshProUGUI priceText = newImage.GetComponentInChildren<TextMeshProUGUI>(true);
                priceText.gameObject.SetActive(true);
                priceText.text = priceText.text.Substring(0, priceText.text.Length-1) + callsData[i].price.ToString();
                newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClick(i, false, imgComponent, priceText.gameObject));
            }
        }
    }

    void OnImageClick(int index, bool isOpen, Image image=null, GameObject priceText=null)
    {
        if (isOpen)
        {
            Debug.Log(callsData.Count.ToString() + index.ToString());
            internalImage.sprite = callsData[index].internalImage;
            internalAudio.clip = callsData[index].internalAudio;
            modalWindow.SetActive(true);
            internalAudio.Play();
        }
        else
        {
            Debug.Log(callsData.Count.ToString() + index.ToString());
            if (data.points >= callsData[index].price)
            {
                data.points -= callsData[index].price;
                callsData[index].isOpen = true;
                Destroy(priceText);
                image.sprite = callsData[index].externalImage;
                image.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
            }
        }
    }
}