using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MonoState : MonoBehaviour
{
    public MonoStateEvent transitionIn;
    public MonoStateEvent update;
    public MonoStateEvent transitionOut;
}
