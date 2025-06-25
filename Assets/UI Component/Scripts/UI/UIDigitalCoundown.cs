using UnityEngine;
using System.Collections;

/// <summary>
/// A digital countdown UI that extends UICountdown.
/// Displays countdown using text that updates every second.
/// </summary>
public class UIDigitalCoundown : UICountdown
{ 
    // Reference to the coroutine running the countdown
    private Coroutine cdCoroutine;
    // Cached WaitForSeconds for better performance (1-second delay)
    private WaitForSeconds wait1s = new WaitForSeconds(1f);

    /// <summary>
    /// Starts the countdown coroutine. 
    /// Ensures only one coroutine runs at a time.
    /// </summary>
    public override void Run()
    {
        base.Run();

        // Prevent running countdown on inactive GameObject (which causes coroutine issues)
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError($"{gameObject.name} inactive in hierarchy");
            return;
        }
        // Stop existing coroutine if it's running
        if (cdCoroutine != null)
            StopCoroutine(cdCoroutine);

        // Start a new countdown coroutine
        cdCoroutine = StartCoroutine(StartCountdown());
    }

    /// <summary>
    /// Coroutine that handles updating the countdown text once per second.
    /// Invokes onCountdownDone when time reaches zero.
    /// </summary>
    private IEnumerator StartCountdown()
    {
        long time = RemindTime;

        // Optional: show initial time if countdown text is available
        if (countdownTxt != null)
            UpdateCountdownTxt(time);

        while (time > 0)
        {
            yield return wait1s;

            time = RemindTime;
            if (countdownTxt != null)
                UpdateCountdownTxt(time);

            // Trigger event when countdown reaches 0
            if (time <= 0)
            {
                time = 0;
                onCountdownDone?.Invoke();
            }
        }
    }

    /// <summary>
    /// Stops the countdown coroutine if running.
    /// </summary>
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
