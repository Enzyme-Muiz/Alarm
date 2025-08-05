using System;
using System.Collections.Generic;
using System.Threading;
using NAudio.Wave;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.Design;



class TimeListener
{
    //Define a list of times in "HH:mm:ss" format
    public List<string> times = new List<string>
        {
            "20:43:00",
            "23:43:00",
            "13:47:00"
        };

    public void addTime()
    {
        Console.WriteLine("Enter a new time to be alerted:");
        String time = Console.ReadLine();
        String timepattern = @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$";
        Regex regex = new Regex(timepattern);
        if (!regex.IsMatch(time))
        {
            Console.WriteLine("Invalid time format. Please use HH:mm:ss.");
            return;
        }
        else
        {
            if (!times.Contains(time))
            {
                times.Add(time);
            }
        }

    }

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
        timeListener.addTime();
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

