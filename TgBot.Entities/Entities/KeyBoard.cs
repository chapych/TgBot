namespace Entities.Entities;

public class KeyBoard
{
    public string[] Buttons { get; set; }

    public KeyBoard(string[] buttons)
    {
        Buttons = buttons;
    }
}