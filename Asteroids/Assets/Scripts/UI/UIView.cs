using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour , IUIView
{
    public event EventHandler OnStartGame;
    public event EventHandler<UpdateEventArgs> OnUpdate = (sender, e) => { };
    public int Score { set { score.text = value.ToString(); } }
    public int Lives {
        set
        {
            for (int i = 0; i < lives.Length; i++)
            {
                lives[i].gameObject.SetActive(value > i);
            }
        }
    }

    [SerializeField] private Button play;
    [SerializeField] private Text score;
    [SerializeField] private Image[] lives;

    private void Awake()
    {
        play.onClick.AddListener(() => { OnStartGame(this, null); });
    }

    public void GamePlay()
    {
        play.gameObject.SetActive(false);

        score.gameObject.SetActive(true);

        foreach (var life in lives)
            life.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        score.gameObject.SetActive(false);

        foreach (var life in lives)
            life.gameObject.SetActive(false);

        play.gameObject.SetActive(true);
    }

    void Update()
    {
        OnUpdate(this, new UpdateEventArgs(Time.deltaTime));
    }
}
