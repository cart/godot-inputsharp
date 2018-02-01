using Godot;
using System;
using HighHat.Actors;

public interface IInput
{
	event Action<IConvertible> Pressed;
	event Action<IConvertible> Released;
	int DeviceID { get; }
	int InputID { get; }
	float GetAxis(IConvertible action);
	bool IsPressed(IConvertible action);
}