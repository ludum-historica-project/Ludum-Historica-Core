using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class MonoStateEvent
{
    public List<GameEvent> gameEvents = new List<GameEvent>();
    public UnityEvent onRaiseEvent;
    public void Raise()
    {
        for (int i = gameEvents.Count - 1; i >= 0; i--)
        {
            gameEvents[i].Raise();
        }
        onRaiseEvent.Invoke();
    }
}
