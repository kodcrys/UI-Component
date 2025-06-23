using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UICountdown : MonoBehaviour
{
    protected long cdDuration;
    protected long startTime;

    [SerializeField] protected TextMeshProUGUI countdownTxt;
    [SerializeField] protected TimeFormat timeFormat;
    [SerializeField] protected bool useUtcTime;

    public UnityEvent onCountdownDone;

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

    public void SetUpAndRun(long startTime, long cdDuration, bool isAdditive = false)
    {
        if (isAdditive)
            this.cdDuration += cdDuration;
        else
            this.cdDuration = cdDuration;
        this.startTime = startTime;

        Run();
    }

    public void SetUp(long startTime, long cdDuration, bool isAdditive = false)
    {
        if (isAdditive)
            this.cdDuration += cdDuration;
        else
            this.cdDuration = cdDuration;
        this.startTime = startTime;

        long time = RemindTime;
        UpdateCountdownTxt(time);
    }

    public virtual void Run()
    {
        
    }


    public virtual void Stop()
    {

    }

    protected virtual void UpdateCountdownTxt(long timeCd)
    {
        if (countdownTxt == null) return;

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

    public enum TimeFormat
    {
        ss,
        mm_ss,
        hh_mm_ss,
        dd_hh_mm_ss
    }
}
