using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSetSubscriber : MonoBehaviour
{
    public RuntimeSet targetSet;
    private void Awake()
    {
        if (targetSet == null)
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        targetSet.SubscribeObject(gameObject);
    }

    private void OnDisable()
    {
        targetSet.UnSubscribeObject(gameObject);
    }
}
