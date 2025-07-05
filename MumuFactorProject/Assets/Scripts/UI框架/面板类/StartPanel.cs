using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : BasePanel
{

    public LevelPanel levelPanel;
    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {

    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnContinue()
    {
        gameObject.SetActive(true);
    }



    public void StartGame()
    {
        UISystem.Instance.PushPanel(levelPanel);
    }

    public void EndGame()
    {
        Application.Quit();
    }


    public void SettingGame()
    {
        print("还没有写好");
    }


}
