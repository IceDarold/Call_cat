using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    void OnMouseDown()
    {
        particles.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(Coroutine());
    }


    private IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<CubeGame>().OnCubeClicked(gameObject);
        yield break;
    }
}
