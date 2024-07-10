using Gst;
using System.Runtime.InteropServices;

internal class Program

{
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetDllDirectory(string lpPathName);

    //static void Main(string[] args)

    //{
    //    bool setBinDir = SetDllDirectory(@"D:\GStreamer\1.0\mingw_x86_64\bin");     

    //    if (!setBinDir)
    //    {
    //        Console.WriteLine("Failed to set DLL directory.");
    //        return;
    //    }
    //    // Initialize Gstreamer

    //    Application.Init(ref args);

    //    // Build the pipeline

    //    var pipeline = Parse.Launch("playbin uri=http://download.blender.org/durian/trailer/sintel_trailer-1080p.mp4");

    //    // Start playing

    //    pipeline.SetState(State.Playing);

    //    // Wait until error or EOS

    //    var bus = pipeline.Bus;

    //    var msg = bus.TimedPopFiltered(Constants.CLOCK_TIME_NONE, MessageType.Eos | MessageType.Error);

    //    // Free resources

    //    pipeline.SetState(State.Null);

    //}

    static void Main(string[] args)
    {
        // Set the GStreamer bin directory
        bool setBinDir = SetDllDirectory(@"D:\GStreamer\1.0\mingw_x86_64\bin");

        if (!setBinDir)
        {
            Console.WriteLine("Failed to set DLL directory.");
            return;
        }

        // Initialize GStreamer
        Application.Init(ref args);

        // Create the pipeline for capturing video from the webcam and saving it to a file
        string pipelineDescription = "ksvideosrc ! videoconvert ! x264enc ! mp4mux ! filesink location=output.mp4";

        var pipeline = Parse.Launch(pipelineDescription) as Pipeline;

        if (pipeline == null)
        {
            Console.WriteLine("Failed to create pipeline.");
            return;
        }

        // Start playing the pipeline
        pipeline.SetState(State.Playing);

        // Wait until error or EOS
        var bus = pipeline.Bus;

        var msg = bus.TimedPopFiltered(Constants.CLOCK_TIME_NONE, MessageType.Eos | MessageType.Error);

        // Handle errors
        if (msg != null && msg.Type == MessageType.Error)
        {
            GLib.GException error;
            string debugInfo;
            msg.ParseError(out error, out debugInfo);
            Console.WriteLine($"Error received from element {msg.Src.Name}: {error.Message}");
            Console.WriteLine($"Debugging information: {debugInfo ?? "none"}");
        }

        // Free resources
        pipeline.SetState(State.Null);
        pipeline.Dispose();
    }


}