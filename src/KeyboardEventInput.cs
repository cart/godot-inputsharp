using Godot;
using System;

namespace HighHat.Input
{
	public class KeyboardEventInput : InputEventInput
	{
		public KeyboardEventInput(int inputID, int deviceID) : base(inputID, deviceID)
		{
		}

		public override float? HandleEventInternal(InputEvent @event)
		{
			if (@event is InputEventKey keyEvent)
			{
				return keyEvent.Pressed ? 1.0f : 0.0f;
			}

			return null;
		}
	}
}