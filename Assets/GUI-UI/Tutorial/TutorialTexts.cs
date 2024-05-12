using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTexts : MonoBehaviour
{
    public Tutorial[] tutorials;
    public TextMeshProUGUI Text;

    private int currentSequence = 0;

    private void Start()
    {
        ShowTutorial(currentSequence);
    }

    public void nextTutorial()
    {
        currentSequence++;
        if (currentSequence < tutorials.Length)
        {
            ShowTutorial(currentSequence);
        }
        else
        {
            currentSequence = tutorials.Length - 1;
        }
    }

    public void prevTutorial()
    {
        currentSequence--;
        if (currentSequence >= 0)
        {
            ShowTutorial(currentSequence);
        }
        else
        {
            currentSequence = 0;
        }
    }

    private void ShowTutorial(int index)
    {
        Text.text = tutorials[index].text;
    }
}