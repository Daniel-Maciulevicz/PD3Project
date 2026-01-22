using System;

public interface IHUDElement
{
    public int HUDNumber { get; set; }

    public event EventHandler<HUDValueChangedArgs> HealthProgressChanged;
    public event EventHandler<HUDValueChangedArgs> ReloadProgressChanged;
}

public class HUDValueChangedArgs : EventArgs
{
    public float NewValue { get; private set; }

    public HUDValueChangedArgs(float newValue)
    {
        NewValue = newValue;
    }
}