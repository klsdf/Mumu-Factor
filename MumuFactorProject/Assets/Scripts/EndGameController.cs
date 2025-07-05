using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogSystem.Instance.SetAndPlayStorys(new List<StoryScript>() { 
            new StoryScript("您获得了最终的胜利"),
            new StoryScript("您击败了所有的咚咚因子和所有的模因"),
            new StoryScript("最终让可可爱爱大陆上的所有群落都感染上了您的碱基片段"),
            new StoryScript("但是对于他们来说，也许这都是他们自己的群体意识"),
             new StoryScript("但是只有您知道，实际上基因才是控制所有生物的核心"),
             new StoryScript("无论群体还是个体，都不过是您的容器罢了"),
             new StoryScript("他们还会被蒙在鼓里多久呢？一天还是一年？"),
             new StoryScript("也许我们永远也无法摆脱基因的奴役。"),
              new StoryScript("游戏介绍。",()=>{
                SceneManager.LoadScene(0);
              }),
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DialogSystem.Instance.PlayNextStory();
        }
    }
}
