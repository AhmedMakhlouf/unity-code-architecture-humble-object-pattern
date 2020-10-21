using System;

public interface IUIView : IUpdatable
{
    event EventHandler OnStartGame;

    int Score { set; }
    int Lives { set; }

    void GamePlay();
    void MainMenu();
}