using UnityEngine;
using TMPro;

public class ButtonLanguageHandler : MonoBehaviour
{
    public string Ru;
    public string Ro;
    void Start()
    {
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = CommandLineArgsHandler.Lang == "Rus" ? Ru : Ro;
    }
}
