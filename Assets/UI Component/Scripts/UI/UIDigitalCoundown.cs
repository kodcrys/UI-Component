using UnityEngine;
using System.Collections;

public class UIDigitalCoundown : UICountdown
{
    private Coroutine cdCoroutine;
    private WaitForSeconds wait1s = new WaitForSeconds(1f);

    public override void Run()
    {
        base.Run();
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError($"{gameObject.name} inactive in hierarchy");
            return;
        }
        if (cdCoroutine != null)
            StopCoroutine(cdCoroutine);
        cdCoroutine = StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        long time = RemindTime;

        if(countdownTxt != null)
            UpdateCountdownTxt(time);

        while (time > 0)
        {
            yield return wait1s;

            time = RemindTime;
            if (countdownTxt != null)
                UpdateCountdownTxt(time);

            if (time <= 0)
            {
                time = 0;
                onCountdownDone?.Invoke();
            }
        }
    }

    public override void Stop()
    {
        base.Stop();
        if (cdCoroutine != null)
        {
            StopCoroutine(cdCoroutine);
            cdCoroutine = null;
        }
    }
}
