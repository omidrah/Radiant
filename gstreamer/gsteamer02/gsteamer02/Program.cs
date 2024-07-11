﻿
using System;
using Gst;
using Gst.App;
using gsteamer02;

class Program
{
    private static string source = "http://download.blender.org/durian/trailer/sintel_trailer-1080p.mp4";
    private static string sourceOptions = string.Empty;

    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            source = args[0];
            if (args.Length > 1)
            {
                sourceOptions = string.Join(' ', args.AsSpan(1).ToArray());
            }
        }

        int scenario = 0;
        do
        {
            Console.Write($"{Environment.NewLine}Which scenario would you like to run?{Environment.NewLine}" +
                           "\tP - playback, R - raw samples, C - complex example, E - exit the app (Default - E): ");
            string answer = Console.ReadLine().Trim().ToUpper();

            switch (answer)
            {
                case "P":
                    scenario = 1;
                    break;
                case "R":
                    scenario = 2;
                    break;
                case "C":
                    scenario = 3;
                    break;
                case "":
                case "E":
                    scenario = -1;
                    break;
                default:
                    break;
            }
        }
        while (scenario == 0);

        switch (scenario)
        {
            case -1:
                Console.WriteLine("Goodbye!");
                return;
            case 1:
                PlaybackOnly.Run(ref args, source, sourceOptions);
                break;
            case 2:
                RawSamplesOnly.Run(ref args, source, sourceOptions);
                break;
            case 3:
                RawSamplesAndPlaybackComplex.Run(ref args, source, sourceOptions);
                break;
            default:
                System.Diagnostics.Debug.Fail("Invalid scenario");
                break;
        }


    }


}