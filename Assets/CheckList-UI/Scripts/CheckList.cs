using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CheckList : MonoBehaviour
{
    public int minLine;
    public int maxLine;

    public float fadeDuration;

    [SerializeField] private List<TextMeshProUGUI> lines;
    [SerializeField] private List<StealableObject> objects;

    private void Start()
    {
        if (lines.Count != maxLine)
        {
            return;
        }

        int lineCount = Random.Range(minLine,maxLine+1);

        MakeLinesVisible(lineCount);

    }

    void MakeLinesVisible(int lineCount)
    {
        for(int i = 0; i < lineCount; i++)
        {
            lines[i].DOFade(1, fadeDuration);
            
        }
    }

    void SelectObjectToSteal()
    {
        int c = 0;
        while (c < lines.Count)
        {

            int sel = Random.Range(0,objects.Count);

            
        }
    }

}
