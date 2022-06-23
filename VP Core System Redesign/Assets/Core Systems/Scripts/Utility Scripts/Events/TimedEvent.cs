using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    public float time;
    public UnityEvent timedEvent;
    void Start()
    {
        StartCoroutine(throwEvent());
    }

    IEnumerator throwEvent()
    {
        yield return new WaitForSeconds(time);
        timedEvent.Invoke();
    }

}
