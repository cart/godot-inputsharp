using Godot;

public interface IInputFactory
{
	IInput ConvertEventToInput(InputEvent inputEvent);
	int GetInputID(InputEvent inputEvent);
}