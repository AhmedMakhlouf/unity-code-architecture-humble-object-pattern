using System;

public interface IUpdatable
{
    event EventHandler<UpdateEventArgs> OnUpdate;
}