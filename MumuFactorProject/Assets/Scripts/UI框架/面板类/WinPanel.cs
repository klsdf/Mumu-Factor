using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel
{
    public override void OnExit()
    {

    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public override void OnPause()
    {

    }

    public override void OnContinue()
    {

    }


    public void NextLevel()
    {

        string  nowLevel = SceneManager.GetActiveScene().name;
        if(nowLevel == "2-5")
        {
            SceneManager.LoadScene("结束场景");
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SaveSystem.Instance.levelNum++;
        SaveSystem.Instance.SaveByJson();



    }
    public void GoBackToTitle()
    {
        SceneManager.LoadScene(0);
    }

}
