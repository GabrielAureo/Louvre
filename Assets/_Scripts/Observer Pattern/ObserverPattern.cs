using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void OnNotify(NotifyArg arg);
}

public interface ISubject
{
    void AddObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void Notify(NotifyArg arg);
}

public class NotifyArg
{
    public string stringArg;

    public NotifyArg(string stringArg)
    {
        this.stringArg = stringArg;
    }

    public NotifyArg()
    {
        this.stringArg = null;
    }
}