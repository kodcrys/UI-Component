using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base UI countdown class that displays remaining time using text.
/// Supports UTC or local time, various time formats, and event callback when finished.
/// </summary>
public class UICountdown : MonoBehaviour
{
    // Total countdown duration in seconds
    protected long cdDuration;

    // Start time of the countdown (Unix timestamp in seconds)
    protected long startTime;

    // Reference to the text component displaying the countdown
    [SerializeField] protected TextMeshProUGUI countdownTxt;
    // Chosen time display format
    [SerializeField] protected TimeFormat timeFormat;
    // Whether to use UTC time or local device time
    [SerializeField] protected bool useUtcTime;

    // Event triggered when countdown reaches zero
    public UnityEvent onCountdownDone;

    // Remaining time in seconds (rounded, integer)
    protected long RemindTime
    {
        get
        {
            if (useUtcTime)
                return startTime.TimeRemainUtc(cdDuration);
            else
                return startTime.TimeRemainLocal(cdDuration);
        }
    }

    // Remaining time in seconds (precise, with decimal part)
    protected float RemindTimePrecise
    {
        get
        {
            if(useUtcTime)
                return startTime.TimeRemainUtcPrecise(cdDuration);
            else
                return startTime.TimeRemainLocalPrecise(cdDuration);
        }
    }

    /// <summary>
    /// Set countdown duration and start immediately. Can add to existing time if needed.
    /// </summary>
    public virtual void SetUpAndRun(long cdDuration, bool isAdditive = false)
    {
        if (isAdditive)
            this.cdDuration += cdDuration;
        else
            this.cdDuration = cdDuration;

        Run();
    }

    /// <summary>
    /// Set countdown duration only. Doesn't start automatically.
    /// </summary>
    public virtual void SetUp(long cdDuration, bool isAdditive = false)
    {
        if (isAdditive)
            this.cdDuration += cdDuration;
        else
            this.cdDuration = cdDuration;
    }

    /// <summary>
    /// Starts the countdown if not already running or if time has expired.
    /// </summary>
    public virtual void Run()
    {
        if (RemindTime <= 0)
        {
            if (useUtcTime)
                startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            else
                startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }

    /// <summary>
    /// Stops or pauses the countdown (can be overridden).
    /// </summary>
    public virtual void Stop()
    {
        // Implement in derived classes if needed
    }

    /// <summary>
    /// Updates the countdown text according to the selected time format.
    /// </summary>
    /// <param name="timeCd">Remaining time in seconds</param>
    protected virtual void UpdateCountdownTxt(long timeCd)
    {
        switch (timeFormat)
        {
            case TimeFormat.ss:
                countdownTxt.text = timeCd.ToString();
                break;
            case TimeFormat.mm_ss:
                countdownTxt.text = timeCd.ToStringTime_M_S();
                break;
            case TimeFormat.hh_mm_ss:
                countdownTxt.text = timeCd.ToStringTime_H_M_S();
                break;
            case TimeFormat.dd_hh_mm_ss:
                countdownTxt.text = timeCd.ToStringTime_D_H_M_S();
                break;
        }
    }

    // Supported time display formats.
    public enum TimeFormat
    {
        ss,              // Seconds only (e.g., "45")
        mm_ss,           // Minutes:Seconds (e.g., "02:15")
        hh_mm_ss,        // Hours:Minutes:Seconds (e.g., "01:03:30")
        dd_hh_mm_ss      // Days:Hours:Minutes:Seconds (e.g., "1d 04:20:00")
    }
}
