using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //暂停游戏
        Time.timeScale = 0;
        PlayStory();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DialogSystem.Instance.PlayNextStory();
        }
    }


    public void PlayStory()
    {
        //print(SceneManager.GetActiveScene().buildIndex);

        List<StoryScript> story = new List<StoryScript>();

        switch (SceneManager.GetActiveScene().name)
        {
            
            case "1-1"://1-1 初识游戏
                story = new List<StoryScript>() {


                    new StoryScript("将姆姆们散播到各个细胞中去吧！"),
                    new StoryScript("鼠标左键滑动来选中细胞，对敌人右键可以释放姆姆因子。"),
                    new StoryScript("打败所有敌人即可获胜！"),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };
                break;

            case "1-2"://1-2 第一次认识高生产速率细胞
                story = new List<StoryScript>() {
                    new StoryScript("在大陆上生活的不仅仅有，也有其他的个体。"),
                    new StoryScript("小绿有更快的姆姆因子生产效率。"),
                    new StoryScript("在对抗咚咚因子的时候，请善用这些个体！"),
                    new StoryScript("要知道，您的基因才是最重要的，可以考虑将这些生产快的细胞因子贡献给其他个体！"),
                    new StoryScript("对着自己人右键，可以共享自己的细胞因子！"),
                    new StoryScript("对于姆姆因子来说，个体毫无价值，只有扩散到更多个体上，才是我们的意义！"),
                    new StoryScript("让姆姆们获得胜利！"),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };
                break;


            case "1-3"://1-3 第一次认识高移动速度细胞
                story = new List<StoryScript>() {
                    new StoryScript("姆姆们又发现了一种新的个体！"),
                    new StoryScript("小红拥有着更高的移动速度。"),
                    new StoryScript("在对抗咚咚因子的时候，请善用这些个体！"),
                    new StoryScript("让姆姆们获得胜利！"),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };
                break;


            case "1-4"://1-4 第一次认识大容量细胞
                story = new List<StoryScript>() {
                    new StoryScript("姆姆们又发现了一种新的个体！"),
                    new StoryScript("小蓝拥有着更高的存储容量，将其作为战略基地是一个不错的选择。"),
                    new StoryScript("在对抗咚咚因子的时候，请善用这些个体！"),
                    new StoryScript("让姆姆们获得胜利！"),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };
                break;

            case "1-5"://第一次认识高产量细胞
                story = new List<StoryScript>() {
                    new StoryScript("姆姆们又发现了一种新的个体！"),
                    new StoryScript("小粉拥有着更高的生产产量。"),
                    new StoryScript("每一次可以获得非常多的数量，但是生产的频率较慢。"),
                    new StoryScript("不过平均下来，比其他细胞生产快很多。"),
                    new StoryScript("在对抗咚咚因子的时候，请善用这些个体！"),
                    new StoryScript("让姆姆们获得胜利！"),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };
                break;

            case "1-6":
            case "1-7":
                Time.timeScale = 1f;
                break;


            case "2-1"://第一次进入到种群层面
                story = new List<StoryScript>() {
                    new StoryScript("姆姆们已经能够非常熟练地寄生个体了！"),
                    new StoryScript("但是，它们的显然野心显然不止于此。"),
                    new StoryScript("哦，或许应该这么说。"),
                    new StoryScript("可可爱爱星球上，拥有姆姆因子和拥有咚咚因子的居民已经完全被控制了！"),
                    new StoryScript("接下来！您将要操纵被因子寄生的群落，进行种群之间的战争！"),

                  new StoryScript("突然！！！！！"),
                    new StoryScript("一股奇妙的感觉从您四周发散出来。"),
                    new StoryScript("明明已经被暗中操纵的群体，居然有部分个体诞生出了反抗意识！"),
                    new StoryScript("您仔细调查了一番。"),
                    new StoryScript("原来是一种叫做<b>模因(meme)</b>的东西。"),
                    new StoryScript("模因来源于文化，宗教等各个途径。"),
                    new StoryScript("在细胞层面战斗时，没有形成模因。"),
                    new StoryScript("但是当个体数量来到群落级别的时候，模因就会产生！"),
                    new StoryScript("模因有很多种。"),
                    new StoryScript("不过，您目前要面对的是一种叫做“丁克主义”的模因。",()=>{
                        print("进入");
                       MemeController[] temp =  GameObject.FindObjectsOfType<MemeController>();
                        foreach(MemeController memeController in temp)
                        {
                            print(memeController.gameObject.name);
                            memeController.memeTypes.Add(MemeType.丁克主义);
                        }

                    }),
                    new StoryScript("被丁克主义所感染的群落，将会停止生产。"),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };
                break;

            case "2-2":

                story = new List<StoryScript>() {
                    new StoryScript("这一次，群落中产生了更加强劲的模因！！"),
                    new StoryScript("是一种叫做悲观主义的恐怖因子。"),
                    new StoryScript("受到悲观主义影响的因子，其增长数率不增反降！！！"),
                    new StoryScript("不过，您目前要面对的是一种叫做“丁克主义”的模因。",()=>{
                        print("进入");
                       MemeController[] temp =  GameObject.FindObjectsOfType<MemeController>();
                        foreach(MemeController memeController in temp)
                        {
                            print(memeController.gameObject.name);
                            memeController.memeTypes.Add(MemeType.丁克主义);
                            memeController.memeTypes.Add(MemeType.悲观主义);
                        }

                    }),
                    new StoryScript("被丁克主义所感染的群落，将会停止生产。"),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };

                break;
            case "2-3"://第一次懒惰主义

                story = new List<StoryScript>() {
                    new StoryScript("由于您的强力控制，群体中滋生了懒惰主义！"),
                    new StoryScript("懒惰主义下的群体，移动速度会超级慢。"),
                    new StoryScript("请您做好准备。",()=>{
                       MemeController[] temp =  GameObject.FindObjectsOfType<MemeController>();
                        foreach(MemeController memeController in temp)
                        {
                            //print(memeController.gameObject.name);
                            memeController.memeTypes.Add(MemeType.丁克主义);
                            memeController.memeTypes.Add(MemeType.悲观主义);
                            memeController.memeTypes.Add(MemeType.懒惰主义);
                        }

                    }),
                    new StoryScript("开始游戏！",()=>{Time.timeScale = 1f; })
                };

                break;
            case "2-4":
            case "2-5":
                MemeController[] temp = GameObject.FindObjectsOfType<MemeController>();
                foreach (MemeController memeController in temp)
                {
                    //print(memeController.gameObject.name);
                    memeController.memeTypes.Add(MemeType.丁克主义);
                    memeController.memeTypes.Add(MemeType.悲观主义);
                    memeController.memeTypes.Add(MemeType.懒惰主义);
                }
                Time.timeScale = 1f;
                break;
            default:
                Time.timeScale = 1f;
                break;

            
        }


        DialogSystem.Instance.SetAndPlayStorys(story);
    }

}
