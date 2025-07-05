using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class BasePanel : MonoBehaviour
{
    public abstract void OnEnter();
    public abstract void OnExit();

    public abstract void OnPause();


    public abstract void OnContinue();

    //public abstract void OnPause();

}
