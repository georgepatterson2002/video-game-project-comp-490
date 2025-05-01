using UnityEngine;
using TMPro;

public class SupabaseUI : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;
    public SupabaseAuth authHandler;

    public void OnSignUpButton()
    {
        authHandler.email = emailInput.text.Trim();
        authHandler.password = passwordInput.text.Trim();
        authHandler.username = usernameInput.text.Trim();

        Debug.Log("Sign-Up button clicked with email: " + authHandler.email);
        authHandler.SignUp();
    }

    public void OnLoginButton()
    {
        authHandler.email = emailInput.text.Trim();
        authHandler.password = passwordInput.text.Trim();

        Debug.Log("Login button clicked with email: " + authHandler.email);
        authHandler.Login();
    }
}
