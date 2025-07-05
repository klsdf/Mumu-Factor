using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel
{
    public override void OnContinue()
    {
        //gameObject.SetActive(true);
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        //gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        //gameObject.SetActive(false);
    }


    public void RePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackToTitle()
    {
        SceneManager.LoadScene(0);
    }

}
