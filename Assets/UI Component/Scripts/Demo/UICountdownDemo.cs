using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICountdownDemo : MonoBehaviour
{
    [SerializeField] private TMP_InputField countdownInputField;
    [SerializeField] private UIDigitalCoundown uiDigitalCoundown;
    [SerializeField] private UIClassicCountdown uiClassicCoundown;
    [SerializeField] private Toggle additionToggle;
    public void StartCountdown()
    {
        int countdown = int.Parse(countdownInputField.text);
        uiDigitalCoundown.SetUpAndRun(countdown, additionToggle.isOn);
        uiClassicCoundown.SetUpAndRun(countdown, additionToggle.isOn);
    }
}
