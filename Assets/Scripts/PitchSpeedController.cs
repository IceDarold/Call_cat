using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchSpeedController : MonoBehaviour
{
    [SerializeField] private Button pitchIncreaseButton;
    [SerializeField] private Button pitchDecreaseButton;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float deltaPitchIncreasePerClick;
    [SerializeField] private float deltaPitchDecreasePerClick;

    [SerializeField] private float deltaPitchPerSecond;


    private void Awake()
    {
        pitchIncreaseButton.onClick.AddListener(PitchClickIncrease);
        pitchDecreaseButton.onClick.AddListener(PitchClickDecrease);
    }

    private void OnDestroy()
    {
        pitchIncreaseButton.onClick.RemoveListener(PitchClickIncrease);
        pitchDecreaseButton.onClick.RemoveListener(PitchClickDecrease);
    } 
    private void Update()
    {
        if(audioSource.pitch > 1)
        {
            audioSource.pitch -= deltaPitchPerSecond * Time.deltaTime;
        }

        if(audioSource.pitch < 1)
        {
            audioSource.pitch += deltaPitchPerSecond * Time.deltaTime;
        }

        if(Mathf.Abs(audioSource.pitch - 1) < 0.01f )
        {
            audioSource.pitch = 1;
        }
    }

    private void PitchClickIncrease()
    {
        if(audioSource.pitch < 3 && audioSource.pitch >= 1)
        {
            audioSource.pitch += deltaPitchIncreasePerClick;
        }
    }

    private void PitchClickDecrease()
    {
       
        if(audioSource.pitch > -3 && audioSource.pitch <= 1)
        {
            Debug.Log("-");
            audioSource.pitch -= deltaPitchDecreasePerClick;
        }
    }

}
