using System;
using UnityEngine;

public static class DateTimeExtensions
{
    public static long TimeRemainUtc(this long unixTimeStampUtcSave, long cdDuration)
    {
        long timeElapsed = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - unixTimeStampUtcSave;
        long timeRemaining = cdDuration - timeElapsed;

        if (timeRemaining < 0) timeRemaining = 0;
        return timeRemaining;
    }

    public static long TimeRemainLocal(this long unixTimeStampLocalSave, long cdDuration)
    {
        long timeElapsed = DateTimeOffset.Now.ToUnixTimeSeconds() - unixTimeStampLocalSave;
        long timeRemaining = cdDuration - timeElapsed;

        if (timeRemaining < 0) timeRemaining = 0;
        return timeRemaining;
    }

    public static float TimeRemainUtcPrecise(this long unixTimeStampUtcSave, float cdDuration)
    {
        double now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0; // milisecond -> second
        double elapsed = now - unixTimeStampUtcSave;
        
        float timeRemaining = (float)(cdDuration - elapsed);
        if (timeRemaining < 0) timeRemaining = 0;
        return timeRemaining;
    }

    public static float TimeRemainLocalPrecise(this long unixTimeStampLocalSave, float cdDuration)
    {
        double now = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0; // milisecond -> second
        double elapsed = now - unixTimeStampLocalSave;

        float timeRemaining = (float)(cdDuration - elapsed);
        if (timeRemaining < 0) timeRemaining = 0;
        return timeRemaining;
    }

    public static string ToStringTime_M_S(this long time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);

        string timeRemainingString = $"{minutes:D2}:{seconds:D2}";
        return timeRemainingString;
    }

    public static string ToStringTime_H_M_S(this long time)
    {
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);

        string timeRemainingString = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        return timeRemainingString;
    }

    public static string ToStringTime_D_H_M_S(this long time)
    {
        int days = (int)(time / 86400);
        int hours = (int)((time % 86400) / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);

        string timeRemainingString = $"{days:D2}D:{hours:D2}:{minutes:D2}:{seconds:D2}";
        return timeRemainingString;
    }
}
