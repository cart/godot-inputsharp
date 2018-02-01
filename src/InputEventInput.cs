using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class InputEventInput : Node, IInput
{
	public int InputID { get; set; }
	public int DeviceID { get; set; }
	public Dictionary<IConvertible, float> Actions = new Dictionary<IConvertible, float>();
	public Dictionary<IConvertible, InputEvent> InputMap = new Dictionary<IConvertible, InputEvent>();
	public Dictionary<IConvertible, Tuple<IConvertible, IConvertible>> VirtualAxes = new Dictionary<IConvertible, Tuple<IConvertible, IConvertible>>();
	private float _actionDeadZone = 0.8f;

	public event Action<IConvertible> Pressed;
	public event Action<IConvertible> Released;

	public InputEventInput(int inputID, int deviceID)
	{
		InputID = inputID;
		DeviceID = deviceID;
	}

	public void RegisterMap<T>() where T : IConvertible
	{
		var enumType = typeof(T);
		foreach (var actionName in Enum.GetNames(enumType))
		{
			foreach (var inputEvent in Godot.InputMap.GetActionList(actionName).OfType<InputEvent>())
			{
				if (HandleEvent(inputEvent).HasValue)
				{
					var enumValue = (T)Enum.Parse(enumType, actionName);
					InputMap.Add(enumValue, inputEvent);
					Actions[enumValue] = 0.0f;
				}
			}
		}
	}

	public float? HandleEvent(InputEvent inputEvent)
	{
		if (inputEvent.Device == DeviceID)
		{
			var eventValue = HandleEventInternal(inputEvent);
			return eventValue;	
		}

		return null;
	}

	public abstract float? HandleEventInternal(InputEvent @event);
	public override void _Input(InputEvent @event)
	{
		var eventValue = HandleEvent(@event);
		if (eventValue.HasValue == false)
		{
			return;
		}

		foreach (var mapping in InputMap)
		{
			if (mapping.Value.ActionMatch(@event))
			{
				if (eventValue.HasValue)
				{
					var pressed = IsPressed(mapping.Key);
					var willBePressed = IsInputValuePressed(eventValue.Value);
					if (pressed && willBePressed == false) // just released
					{
						Released?.Invoke(mapping.Key); 
					}
					else if (pressed == false && willBePressed) // just pressed
					{
						Pressed?.Invoke(mapping.Key); 
					}

					Actions[mapping.Key] = eventValue.Value;
				}
			}
		}
	}

	public void AddVirtualAxis(IConvertible name, IConvertible negativeAction, IConvertible positiveAction)
	{
		VirtualAxes[name] = new Tuple<IConvertible, IConvertible>(negativeAction, positiveAction);
	}

	public float GetAxis(IConvertible action)
	{
		if (Actions.TryGetValue(action, out float axisAction))
		{
			return axisAction;
		}
		else if (VirtualAxes.TryGetValue(action, out Tuple<IConvertible, IConvertible> virtualAxis))
		{
			var leftAxis = GetAxis(virtualAxis.Item1);
			var rightAxis = GetAxis(virtualAxis.Item2);

			return Math.Abs(rightAxis) - Math.Abs(leftAxis);
		}

		throw new Exception($"Action \"{action}\" does not exist");
	}

	public bool IsPressed(IConvertible action)
	{
		if (Actions.TryGetValue(action, out float pressedAction))
		{
			return IsInputValuePressed(pressedAction);
		}

		throw new Exception($"Action \"{action}\" does not exist");
	}

	private bool IsInputValuePressed(float inputValue)
	{
		return inputValue > _actionDeadZone;
	}
}