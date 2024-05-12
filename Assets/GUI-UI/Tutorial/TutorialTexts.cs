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
        Text.text = tutorials[currentSequence].text;


    }
    private void Update()
    {
        
    }
    public void nextTutorial()
    {
        currentSequence++;
        Text.text = tutorials[currentSequence].text;
    }
    
}
