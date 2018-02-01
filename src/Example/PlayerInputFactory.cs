using Godot;

// This is an example of how I use it
public class PlayerInputFactory : InputFactory
{
	public override IInput ConvertEventToInput(InputEvent inputEvent)
	{
		var input = base.ConvertEventToInput(inputEvent);
		if (input is InputEventInput inputEventInput)
		{
			inputEventInput.RegisterMap<PlayerInput>();
			inputEventInput.AddVirtualAxis(PlayerInput.Horizontal, PlayerInput.Left, PlayerInput.Right);
		}

		// you would probably do these things elsewhere, but here are some usage examples:

		// connect a signal 
		input.Pressed += action => GD.Print($"{action} was just pressed");

		// check the current state
		if (input.IsPressed(PlayerInput.Left))
		{
			GD.Print($"{nameof(PlayerInput.Left)} is currently pressed")
		}

		return input;
	}
}