
using System;

public interface IInputReader
{
    event EventHandler<InputEventArgs> OnInput;
}