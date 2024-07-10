using Gst;
using System.Runtime.InteropServices;

internal class Program

{
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetDllDirectory(string lpPathName);

    static void Main(string[] args)

    {
        bool setBinDir = SetDllDirectory(@"D:\GStreamer\1.0\mingw_x86_64\bin");     

        if (!setBinDir)
        {
            Console.WriteLine("Failed to set DLL directory.");
            return;
        }
        // Initialize Gstreamer

        Application.Init(ref args);

        // Build the pipeline

        var pipeline = Parse.Launch("playbin uri=http://download.blender.org/durian/trailer/sintel_trailer-1080p.mp4");

        // Start playing

        pipeline.SetState(State.Playing);

        // Wait until error or EOS

        var bus = pipeline.Bus;

        var msg = bus.TimedPopFiltered(Constants.CLOCK_TIME_NONE, MessageType.Eos | MessageType.Error);

        // Free resources

        pipeline.SetState(State.Null);

    }

}