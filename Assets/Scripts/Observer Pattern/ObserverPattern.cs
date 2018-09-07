using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void OnNotify();
}

public interface ISubject
{
    void AddObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void Notify();
}