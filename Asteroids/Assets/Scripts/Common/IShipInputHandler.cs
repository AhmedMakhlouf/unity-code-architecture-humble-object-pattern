using System;

public interface IShipInputHandler
{
    event EventHandler<InputEventArgs> OnInput;
}