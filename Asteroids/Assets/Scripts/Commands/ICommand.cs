using UnityEngine;
using System.Collections;

public interface ICommand
{
    void Excute(IMovable movement, IGun gun);
}