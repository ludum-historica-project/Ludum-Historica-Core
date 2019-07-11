using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    private bool _paused;
    public bool Pause
    {
        get
        {
            return _paused;
        }
        set
        {
            if (_paused != value)
            {
                _paused = value;

                Time.timeScale = _paused ? 0 : 1;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
        }
    }
}
