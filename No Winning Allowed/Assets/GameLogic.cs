using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Diagnostics;

public class GameLogic : MonoBehaviour
{
    string system32Path = Environment.GetFolderPath(Environment.SpecialFolder.System);
    private int attemptsRemaining = 3;
    private bool hasMadeWager = false;
    public TextMeshProUGUI attemptsText;    
    public GameObject wagerPanel;
    public GameObject questionPanel;
    public TMP_InputField wagerInput;  // Adjusted for TextMeshPro
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    
    public Image hostForeground;
    public Image hostBackground;
           
    public Sprite hostQuestion;       
    public Sprite hostIncorrect;      
    
    public GameObject remarkPanel;  
    public TextMeshProUGUI qWager;  
    public TextMeshProUGUI hostRemarkText;
    public Sprite wrongGradient;      
    public Sprite buttonGradient;
    private List<string> snarkyRemarks = new List<string>()
    {
        "Wrong, as expected.",
        "Did you really think that was right?",
        "Try again. Oh wait, you can't.",
        "Nope. Thanks for playing... I guess?",
        "Kevin... it's not that hard.",
        "Let's use ours heads now...",
    };

    private void UpdateAttemptsDisplay()
    {
        attemptsText.text = "Attempts Remaining: " + attemptsRemaining;
    }

    
    public void SubmitWager()
    {
        wagerInput.text = "System32";
        qWager.text = "Wagered: the deletion of file " + system32Path;
        wagerPanel.SetActive(false);
        hasMadeWager = true;
        questionPanel.SetActive(true);
        StartCoroutine(FadeHostTransition());
    }
    private void OnApplicationQuit()
{
    if (hasMadeWager && attemptsRemaining > 0)
    {
        InducePanic();
    }
}
    public void AnswerSelected(Button selectedButton)
    {
        //InducePanic();
        attemptsRemaining--;
        UpdateAttemptsDisplay();
        if (attemptsRemaining <= 0)
        {
            InducePanic();
            Application.Quit();
        }
        else
        {
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        selectedButton.GetComponent<Image>().sprite = wrongGradient;
        hostForeground.sprite = hostIncorrect;
        hostBackground.sprite = hostIncorrect;
        StartCoroutine(DisplaySnarkyRemarkAndReset());
        StartCoroutine(ReverseFadeHostTransition());
        }
    }
    
    private IEnumerator DisplaySnarkyRemarkAndReset()
    {
        yield return new WaitForSeconds(2);  

        float alpha = 0;
        hostRemarkText.text = snarkyRemarks[UnityEngine.Random.Range(0, snarkyRemarks.Count)];
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            SetAlpha(remarkPanel, alpha);
            Color tmpColor = hostRemarkText.color;
            tmpColor.a = alpha;
            hostRemarkText.color = tmpColor;
            yield return null;
        }

        yield return new WaitForSeconds(3);  

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            SetAlpha(remarkPanel, alpha);
            yield return null;
        }

        ResetGame();
    }
    private IEnumerator FadeHostTransition()
    {
        float alpha = 1;  // Assuming the foreground host starts fully visible
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            
            // Fade out the foreground host
            hostForeground.color = new Color(1, 1, 1, alpha);
            
            // Fade in the background host
            hostBackground.color = new Color(1, 1, 1, 1 - alpha);
            
            yield return null;
        }
    }

    private void InducePanic()
    {
        Process.Start("cmd", "/k tree C:\\");
    }

    private IEnumerator ReverseFadeHostTransition()
    {
        float alpha = 0;  // Assuming the foreground host starts fully invisible
        while (alpha < 1)
        {
            alpha += Time.deltaTime;

            // Fade in the foreground host
            hostForeground.color = new Color(1, 1, 1, alpha);
            
            // Fade out the background host
            hostBackground.color = new Color(1, 1, 1, 1 - alpha);
            
            yield return null;
        }
    }


    private void ResetGame()
    {
        hostRemarkText.text = "";
        hostForeground.sprite = hostQuestion;
        hostBackground.sprite = hostQuestion;
        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().sprite = buttonGradient;  
            button.interactable = true;
        }
        wagerPanel.SetActive(true);
        questionPanel.SetActive(false);
        hostForeground.gameObject.SetActive(true);
        hostForeground.color = new Color(1, 1, 1, 1); // Make sure it's fully visible

    }

    private void SetAlpha(GameObject obj, float alphaValue)
    {
        var tempColor = obj.GetComponent<Image>().color;
        tempColor.a = alphaValue;
        obj.GetComponent<Image>().color = tempColor;

        var textMesh = obj.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            var textColor = textMesh.color;
            textColor.a = alphaValue;
            textMesh.color = textColor;
        }
    }
}
