using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class Switch : ICommand
{
    Access receiver;
    public Switch(Access r) => receiver = r;
    public void Execute() => receiver.Active();
    public void Undo() => receiver.Inaccessible();
}
 
public class Hex : MonoBehaviour
{
    public Node node;
    Command command;
    public void SetCommand(Command c) => command = c;
    public void Run() => command.Execute();
    public void Cancel() => command.Undo();
    
    void OnMouseUpAsButton()
    {
        Run();
        bool active = FindObjectOfType<GameManager>().check(node);
        gameObject.SetActive(active);
        Cancel();
    }

    void Start()
    {
        var command = new Switch(FindObjectOfType<Access>());
        SetCommand(command);
    }
}
