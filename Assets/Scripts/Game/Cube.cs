using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown()
    {
        if (FindObjectOfType<CubeGame>().OnCubeClicked(gameObject))
        {
            StartCoroutine(Coroutine());
        }
    }


    private IEnumerator Coroutine()
    {
        particles.Play();
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        yield break;
    }
}
