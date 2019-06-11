using System.Collections.Generic;
using System;

public interface IObservable
{
    void AddObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObservers(string s);
}
 
public interface IObserver
{
    void Upd(string s);
}

public class StateOfGame : IObservable
{
    public bool lose = false;
    public bool win = false;
    private List<IObserver> observers;

    public StateOfGame() => observers = new List<IObserver>();
    public void AddObserver(IObserver o) => observers.Add(o);
    public void RemoveObserver(IObserver o) => observers.Remove(o);
    
    public void setLose(bool lose) 
    {
        this.lose = lose;
        NotifyObservers("lose");
    }
    public void setWin(bool win) 
    {
        this.win = win;
        NotifyObservers("win");
    }

    public void NotifyObservers(string s)
    {
        foreach (IObserver observer in observers)
            observer.Upd(s);
    } 
}
