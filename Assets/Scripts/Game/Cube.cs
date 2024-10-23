using UnityEngine;

public class Cube : MonoBehaviour
{
    void OnMouseDown()
    {
        FindObjectOfType<CubeGame>().OnCubeClicked(gameObject);
    }
}
