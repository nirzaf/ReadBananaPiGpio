using System.Device.Gpio;

namespace Gpio;

public static class Program
{
    const string Alert = "ALERT 🚨";
    const string Ready = "READY ✅";
    
    public static void Main()
    {
        const int Pin = 21; // Change this to your desired GPIO pin number
        using var controller = new GpioController();
        controller.OpenPin(Pin, PinMode.InputPullUp);

        Console.WriteLine($"Initial status ({DateTime.Now}): {(controller.Read(Pin) == PinValue.High ? Alert : Ready)}");

        controller.RegisterCallbackForPinValueChangedEvent(Pin, PinEventTypes.Falling | PinEventTypes.Rising, OnPinEvent);

        Task.Delay(Timeout.Infinite).Wait();
    }

    static void OnPinEvent(object sender, PinValueChangedEventArgs args)
    {
        string status = args.ChangeType == PinEventTypes.Rising ? Alert : Ready;
        Console.WriteLine($"({DateTime.Now}) {status}");
    }
}