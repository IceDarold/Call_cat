using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchSpeedController : MonoBehaviour
{
    [SerializeField] private Button pitchButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float deltaPitchPerClick;
    [SerializeField] private float deltaPitchPerSecond;


    private void Awake()
    {
        pitchButton.onClick.AddListener(PitchClick);
    }

    private void OnDestroy()
    {
        pitchButton.onClick.RemoveListener(PitchClick);
    }
    private void Update()
    {
        if(audioSource.pitch > 1)
        {
            audioSource.pitch -= deltaPitchPerSecond * Time.deltaTime;
        }
    }

    private void PitchClick()
    {
        if(audioSource.pitch < 3)
        {
            audioSource.pitch += deltaPitchPerClick;
        }
    }

}
