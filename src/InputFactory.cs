using Godot;

namespace HighHat.Input
{
	public class InputFactory : IInputFactory
	{
		public const int KeyboardEventIDOffset = 100;
		public const int JoypadEventIDOffset = 200;
		public virtual IInput ConvertEventToInput(InputEvent inputEvent)
		{
			var inputID = GetInputID(inputEvent);
			switch (inputEvent)
			{
				case InputEventKey inputEventKey:
					return new KeyboardEventInput(inputID, inputEvent.Device);
				case InputEventJoypadButton inputEventJoy:
					return new JoypadEventInput(inputID, inputEventJoy.Device);
				default:
					return null;
			}
		}

		public int GetInputID(InputEvent inputEvent)
		{
			switch (inputEvent)
			{
				case InputEventKey inputEventKey:
					return inputEvent.Device + KeyboardEventIDOffset;
				case InputEventJoypadButton inputEventJoy:
					return inputEvent.Device + JoypadEventIDOffset;
				default:
					return inputEvent.Device;
			}
		}
	}
}