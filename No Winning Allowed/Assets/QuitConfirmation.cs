using UnityEngine;
using System.Collections;

public class QuitConfirmation : MonoBehaviour
{
    public GameObject quitConfirmationPanel;
    
    private bool isAttemptingToQuit = false; // This flag is used to check if the user is trying to quit.

    private void Start()
    {
        quitConfirmationPanel.SetActive(false);
    }

    private void Update()
    {
        // Detect Alt+F4 key press
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.F4))
        {
            ShowQuitConfirmation();
        }
    }

    public void ShowQuitConfirmation()
    {
        isAttemptingToQuit = true;
        quitConfirmationPanel.SetActive(true);
        // Optionally, pause the game if you have any active gameplay
        // Time.timeScale = 0;
    }

    public void ConfirmQuit()
    {
        Application.Quit();

        // If you are running this in the editor, use the line below
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void CancelQuit()
    {
        isAttemptingToQuit = false;
        quitConfirmationPanel.SetActive(false);
        
        // If you paused the game earlier, resume it
        // Time.timeScale = 1;
    }

    private void OnApplicationQuit()
    {
        if (isAttemptingToQuit)
        {
            CancelQuit();
            Application.CancelQuit(); // Cancel the quit process.
        }
    }
}
