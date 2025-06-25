using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A classic-style countdown UI using a radial image fill (e.g., skill cooldown mask).
/// Extends UICountdown to visually show progress with Image.fillAmount.
/// </summary>
public class UIClassicCountdown : UICountdown
{
    // Reference to the UI Image used for the radial fill (e.g., a cooldown mask)
    [SerializeField] private Image maskCdImg;
    // Whether the countdown is currently running
    private bool isRun;

    /// <summary>
    /// Sets up and starts the countdown. Can add to existing duration if specified.
    /// </summary>
    public override void SetUpAndRun(long cdDuration, bool isAdditive = false)
    {
        isRun = false;
        base.SetUpAndRun(cdDuration, isAdditive);
    }

    /// <summary>
    /// Sets up the countdown duration only (does not start).
    /// </summary>
    public override void SetUp(long cdDuration, bool isAdditive = false)
    {
        isRun = false;
        base.SetUp(cdDuration, isAdditive);
    }

    /// <summary>
    /// Starts the countdown, activates the mask, and sets initial UI state.
    /// </summary>
    public override void Run()
    {
        base.Run();

        isRun = true;
        maskCdImg.gameObject.SetActive(true);
        // Start with full mask
        maskCdImg.fillAmount = 1f;

        // Optional: show initial time if countdown text is available
        if (countdownTxt != null)
            UpdateCountdownTxt(RemindTime);
    }

    /// <summary>
    /// Called every frame while the countdown is running.
    /// Updates both the fill mask and the countdown text.
    /// </summary>
    private void Update()
    {
        if (!isRun) return;
        
        UpdateMask();

        // Trigger event when countdown reaches 0
        if (RemindTime <= 0)
        {
            onCountdownDone.Invoke();
            isRun = false;
        }
    }

    /// <summary>
    /// Updates the fill amount based on remaining time and updates countdown text if available.
    /// </summary>
    private void UpdateMask()
    {
        maskCdImg.fillAmount = RemindTimePrecise / cdDuration;

        if (countdownTxt != null)
            UpdateCountdownTxt(RemindTime);
    }
}
