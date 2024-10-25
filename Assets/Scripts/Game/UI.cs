using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject game_visual;
    public CubeGame game;
    public CallUI callUI;
    public void GetBack()
    {
        game.StopGame();
        game_visual.SetActive(false);
        callUI.UpdateUI();
    }
    public void Play()
    {
        callUI.Finish();
        game_visual.SetActive(true);
        game.StartNewGame();
    }
}
