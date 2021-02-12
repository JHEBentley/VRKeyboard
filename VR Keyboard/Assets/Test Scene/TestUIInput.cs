using UnityEngine;
using TMPro;
using VRKeyboard;

public class TestUIInput : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public VRKeyboardManager keyboard;

    void Update()
    {
        textField.text = keyboard._string;
    }
}
