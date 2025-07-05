using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MangaController : MonoBehaviour
{

    public GameObject manga1;
    public GameObject manga2;
    public GameObject manga3;
    public GameObject manga4;
    // Start is called before the first frame update
    void Start()
    {
        //
        DialogSystem.Instance.SetAndPlayStorys(new List<StoryScript>(){
            new StoryScript("最近在可可爱爱大陆，有一件新鲜事儿。"),
            new StoryScript("Vivo碱基片段上的50号基因发生了突变，产生了一种被成为<b>姆姆因子</b>的新基因。",() => {  manga1.GetComponent<Image>().DOFade(1,1); }),
            new StoryScript("这种因子非常的奇妙，被它寄生到的细胞，都会变得更加健康。"),
            new StoryScript("因此受到了可可爱爱大陆居民的热烈欢迎。",() => { manga2.GetComponent<Image>().DOFade(1,1);  }),
            new StoryScript("但是，不幸的是，在第114号碱基片段上，同样产生了一种新的基因。"),
            new StoryScript("咚咚因子。"),
            new StoryScript("这种因子会使得生物个体免疫力下降，加速衰老。"),
            new StoryScript("而且最致命的是——"),
            new StoryScript("咚咚因子正以很快的速度在感染着可可爱爱大陆的居民。"),
            new StoryScript("被感染的居民会来攻击并感染其他的居民。",() => { manga3.GetComponent<Image>().DOFade(1,1);  }),
            new StoryScript("这就会导致一个问题"),
            new StoryScript("那就是，姆姆因子的宿主被咚咚因子霸占了"),
            new StoryScript("因此姆姆因子们就开始操纵宿主来攻击咚咚因子们"),
            new StoryScript("就这样"),
            new StoryScript("咚咚因子和姆姆因子的战争开始了。",() => { manga4.GetComponent<Image>().DOFade(1,1);  }),
            new StoryScript("但是，讽刺的是。明明是有益的基因片段，实际上却做着同样感染的行为。"),
            new StoryScript("将居民的基因替换为姆姆因子，并杀死其他居民身上的咚咚因子。"),
            new StoryScript("可怜的居民并没有发现，他们以为战争是自己的意识。"),
            new StoryScript("实际上，他们不过是一具容器罢了。"),
            new StoryScript("他们更没有意识到的是。"),
            new StoryScript("姆姆因子实际上是拥有意识的。"),
            new StoryScript("他们的意识综合体就是——您"),
            new StoryScript("在接下来的战争中，您将要操纵姆姆因子来控制这个星球上的所有生物。"),
            new StoryScript("让所有生物臣服于您的碱基下吧！"),
             new StoryScript("",()=>{ SceneManager.LoadScene(1); }),
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
