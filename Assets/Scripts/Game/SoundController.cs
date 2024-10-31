using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip RightColor;
        [SerializeField] private AudioClip WrongColor;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(bool isRight)
        {
            AudioClip clip = isRight ? RightColor : WrongColor;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}