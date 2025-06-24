using TMPro;
using UnityEngine;

public class UICountdownDemo : MonoBehaviour
{
    [SerializeField] private TMP_InputField countdownInputField;
    [SerializeField] private UIDigitalCoundown uiDigitalCoundown;
    [SerializeField] private UIClassicCountdown uiClassicCoundown;

    public void StartCountdown()
    {
        int countdown = int.Parse(countdownInputField.text);
        uiDigitalCoundown.SetUpAndRun(countdown);
        uiClassicCoundown.SetUpAndRun(countdown);
    }
}
