using Godot;
using System;

namespace HighHat.Input
{
	public class JoypadEventInput : InputEventInput
	{
		public JoypadEventInput(int inputID, int deviceID) : base(inputID, deviceID)
		{
		}

		public override float? HandleEventInternal(InputEvent @event)
		{
			switch (@event)
			{
				case InputEventJoypadButton buttonEvent:
					return buttonEvent.Pressed ? 1.0f : 0.0f;
				case InputEventJoypadMotion motionEvent:
					return Mathf.Abs(motionEvent.AxisValue);
				default:
					return null;
			}
		}
	}
}