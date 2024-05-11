using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckListObject
{
    #region PRIVATE
    private string name;
    private int maxCount;
    private int currentCount;
    private TextMeshProUGUI text;
    #endregion

    public CheckListObject(string name, int maxCount, TextMeshProUGUI text)
    {
        this.name = name;
        this.maxCount = maxCount;
        this.text = text;
        UpdateLineText();
    }

    void UpdateLineText()
    {
        text.text = name + "   " + currentCount + "/" + maxCount; 
    }

    public void ObjectHasStolen()
    {
        currentCount++;
        UpdateLineText();

        if (currentCount >= maxCount)
        {
            text.alpha = 0.25f;
            
        }
    }

    public string GetName()
    {
        return name;
    }

    

}
