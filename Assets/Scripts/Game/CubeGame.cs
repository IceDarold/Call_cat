using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class CubeGame : MonoBehaviour
{
    public GameObject cubePrefab; // Префаб кубика
    public Image targetCat;
    public TextMeshProUGUI targetColorText;
    public TextMeshProUGUI needCubesText;
    public int numberOfCubes = 10; // Количество кубиков
    private int points;
    public Transform generationa_area;
    private List<GameObject> cubes = new List<GameObject>();
    private List<GameObject> target_cubes = new List<GameObject>();

    private Color targetColor; // Цвет, который нужно выбрать
    public List<Color> availableColors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow };

    public DataScriptableObject data;
    public SoundController soundController;


    void Start()
    {
        if (availableColors.Count > numberOfCubes)
        {
            throw new System.Exception("availableColors.Count must be less than number of Cubes");
        }
        StartNewGame();
    }


    public void StopGame()
    {
        DataController.SaveData(points);
        ClearCubes();
    }

    public void StartNewGame()
    {
        ClearCubes();
        points = DataController.LoadData();
        int lastIndex = targetColorText.text.LastIndexOf('\n');
        targetColorText.text = targetColorText.text.Substring(0, lastIndex + 1) + points.ToString();
        SetTargetColor();
        SpawnCubes();
    }

    void ClearCubes()
    {
        foreach (GameObject cube in cubes)
        {
            Destroy(cube);
        }
        cubes.Clear();
        target_cubes.Clear();
    }
    void SpawnCube(Color cube_color)
    {
        GameObject cube = Instantiate(cubePrefab, GetRandomPosition(), Quaternion.identity);
        cube.GetComponent<Renderer>().material.color = cube_color;
        if (cube_color == targetColor)
        {
            target_cubes.Add(cube);
        }
        cubes.Add(cube);
    }
    void SpawnCubes()
    {
        foreach (Color color in availableColors)
        {
            SpawnCube(color);
        }
        for (int i = 0; i < numberOfCubes - availableColors.Count; i++)
        {
            Color cube_color = availableColors[Random.Range(0, availableColors.Count)];
            SpawnCube(cube_color);
        }
        needCubesText.text = target_cubes.Count.ToString();
    }

    void SetTargetColor()
    {
        targetColor = availableColors[Random.Range(0, availableColors.Count)];
        targetCat.color = targetColor;
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(
                            generationa_area.position.x - generationa_area.localScale.x / 2,
                            generationa_area.position.x + generationa_area.localScale.x / 2),
                           Random.Range(
                               generationa_area.position.y - generationa_area.localScale.y / 2,
                               generationa_area.position.y + generationa_area.localScale.y / 2), 0);
    }

    public bool OnCubeClicked(GameObject clickedCube)
    {
        bool res = false;
        Color cubeColor = clickedCube.GetComponent<Renderer>().material.color;
        if (cubeColor == targetColor)
        {
            points += 1;
            cubes.Remove(clickedCube);
            target_cubes.Remove(clickedCube);

            res = true;
            
            if (target_cubes.Count == 0)
            {
                DataController.SaveData(points);
                StartNewGame();
            }
        }
        else if (points > 0)
        {
            points -= 1;
        }
        int lastIndex = targetColorText.text.LastIndexOf('\n');
        targetColorText.text = targetColorText.text.Substring(0, lastIndex + 1) + points.ToString();
        needCubesText.text = target_cubes.Count.ToString();
        soundController.PlaySound(res);

        return res;
    }
}
