namespace SuperUltraMegaClick;
using WindowsInput;


public class Clicker
{
    private static readonly InputSimulator InputSimulator = new();
    
    public static void LeftMouseClick(int multiCounter)
    {
        for (int i = 0; i < multiCounter; i++)
        {
            InputSimulator.Mouse.LeftButtonClick();
        }
    }
    public static void RightMouseClick(int multiCounter)
    {
        for (int i = 0; i < multiCounter; i++)
        {
            InputSimulator.Mouse.RightButtonClick();
        }
    }
    
}