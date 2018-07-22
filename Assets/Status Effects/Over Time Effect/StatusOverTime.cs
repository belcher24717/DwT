using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class StatusOverTime : MonoBehaviour
{
    public float Duration;
    public int TickValue;
    public float TickRate;    
    public string Id;

    private DateTime _expireTime;
    private DateTime _lastTick;
    abstract protected void Tick();

    // Use this for initialization
    protected void Start()
    {
        _lastTick = DateTime.Now.AddSeconds(-TickRate);
        _expireTime = DateTime.Now.AddSeconds(Duration);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_expireTime <= DateTime.Now)
            Destroy(this.gameObject);
        else if (DateTime.Now > _lastTick.AddSeconds(TickRate))
            BaseTick();
    }

    public void BaseTick()
    {
        Tick();
        _lastTick = DateTime.Now;
    }

}
