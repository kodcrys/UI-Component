using UnityEngine;
using UnityEngine.UI;

public class UIClassicCountdown : UICountdown
{
    [SerializeField] private Image maskCdImg;
    private bool isRun;

    public override void SetUpAndRun(long cdDuration, bool isAdditive = false)
    {
        isRun = false;
        base.SetUpAndRun(cdDuration, isAdditive);
    }

    public override void SetUp(long cdDuration, bool isAdditive = false)
    {
        isRun = false;
        base.SetUp(cdDuration, isAdditive);
    }

    public override void Run()
    {
        base.Run();

        isRun = true;
        maskCdImg.gameObject.SetActive(true);
        maskCdImg.fillAmount = 1f;

        if (countdownTxt != null)
            UpdateCountdownTxt(RemindTime);
    }

    private void Update()
    {
        if (!isRun) return;
        
        UpdateMask();
        
        if (RemindTime <= 0)
        {
            onCountdownDone.Invoke();
            isRun = false;
        }
    }

    private void UpdateMask()
    {
        maskCdImg.fillAmount = RemindTimePrecise / cdDuration;

        if (countdownTxt != null)
            UpdateCountdownTxt(RemindTime);
    }
}
