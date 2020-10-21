using UnityEngine;
using System.Collections;
using System;

public class InputReader : MonoBehaviour, IInputReader /*IInputReader<IGun>, IInputReader<IMovable>*/
{
    public event EventHandler<InputEventArgs> OnInput;
    //public event EventHandler<InputEventArgs<IGun>> IInputReader<IGun>.OnInput = (sender r) => {};
    //public event EventHandler<InputEventArgs<IMovable>> IInputReader<IMovable>.OnInput;

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool fire = Input.GetKeyDown(KeyCode.Space);
        

        if (vertical != 0 || 
            horizontal != 0 || 
            fire)
        {
            var command = new ShipInputCommand(vertical, horizontal, fire, Time.deltaTime);
            OnInput(this, new InputEventArgs(command));
            //var command = new ChangeVelocityCommand(verticalInput, horizontalInput, Time.deltaTime);
            //var inputArgs = new InputEventArgs<IMovable>(command);
            //OnInput(this, inputArgs);
        }

        //if (fire)
        //{
        //    var command = new FireGunCommand();
        //    var inputArgs = new InputEventArgs<IGun>(command);
        //    //OnInput(this, inputArgs);
        //}
    }
}
