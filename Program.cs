using System;
using System.Collections.Generic;
using System.Threading;
using NAudio.Wave;



class TimeListener
{
    //Define a list of times in "HH:mm:ss" format
    public List<string> times = new List<string>
        {
            "20:43:00",
            "23:43:00",
            "13:47:00"
        };

}   
class Program
{
    public static void player(string filePath)
    {
        using (var audioFile = new AudioFileReader(filePath))
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();

            // Wait until playback finishes
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100); // Sleep to reduce CPU usage
            }
        }

        Console.WriteLine("Playback finished.");
    }
    static void Main(string[] args)
    {


        Console.WriteLine("Program is running...");

        string filePath = @"..\..\..\recording\nabeel2019.mp3";
        
        TimeListener timeListener = new TimeListener();
        List<string> times = timeListener.times;

        while (true)
        {
            // Get the current time in "HH:mm:ss" format
            string currentTime = DateTime.Now.ToString("HH:mm:ss");

                // Check if the current time is in the list
                if (times.Contains(currentTime))
                {
                player(filePath);
                Console.WriteLine("It is time: " + currentTime);
                }

            // Pause for one second to prevent continuous checking
            Thread.Sleep(1000);
        }
    }
}

