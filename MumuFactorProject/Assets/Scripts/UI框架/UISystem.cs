using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PanelType
{
    ItemPanel
}



public class UISystem
{

    private static UISystem instance;
    public static UISystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UISystem();
            }
            return instance;
        }
    }

    private Stack<BasePanel> panels = new Stack<BasePanel>() { };



    public void PopPanel()
    {
        BasePanel temp = PeekPanel();
        temp.OnExit();
        panels.Pop();


    }
    public void PushPanel(BasePanel panel)
    {
        BasePanel tempPanel = PeekPanel();
        tempPanel?.OnPause();
 


        panels.Push(panel);
        panel.OnEnter();
    }

    public BasePanel PeekPanel()
    {
        if (panels.Count == 0)
        {
            return null;
        }
        return panels.Peek();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
