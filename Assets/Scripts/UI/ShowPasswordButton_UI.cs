using UnityEngine;
using TMPro;

public class ShowPasswordButton_UI : MonoBehaviour
{
    public void SwapInputFieldContentType(TMP_InputField inputField)
    {
        if (inputField.contentType == TMP_InputField.ContentType.Password)
            inputField.contentType = TMP_InputField.ContentType.Standard;
        else inputField.contentType = TMP_InputField.ContentType.Password;

        inputField.enabled = false;
        inputField.enabled = true;
    }
}
