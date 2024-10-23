using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject game_visual;
    public GameObject calls_visual;
    public CubeGame game;
    public void GetBack()
    {
        game.StopGame();
        game_visual.SetActive(false);
        calls_visual.SetActive(true);
    }
    public void Play()
    {
        calls_visual.SetActive(false);
        game_visual.SetActive(true);
        game.StartNewGame();
    }
}
