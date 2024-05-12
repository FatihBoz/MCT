using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CheckList : MonoBehaviour
{
    public static CheckList instance;
    public bool canFinish;

    #region PUBLIC
    public int minLine;
    public int maxLine;
    public float fadeDuration;

    public Vector3 publicOffSet;
    public Vector3 privateOffSet;

    public GameObject CheckMark;
    #endregion

    [SerializeField] private List<TextMeshProUGUI> lines;
    [SerializeField] private List<StealableObject> objects;

    #region PRIVATE
    private CheckListObject[] checkListObjects;

    private int lineCount;
    private bool isOpen;
    private Vector3 publicPos;
    private Vector3 privatePos;

    private RectTransform rectTransform;
    #endregion

    private void OnEnable()
    {
        PlayerMovement.OnObjectStolen += CheckList_OnObjectStolen;
    }

    private void Awake()
    {
        instance = this;
        SetOffSet();
        checkListObjects = new CheckListObject[objects.Count];

    }

    private void Start()
    {
        if (lines.Count != maxLine)
        {
            return;
        }

        SelectObjectToSteal();

        lineCount = Random.Range(minLine,maxLine+1);

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

            CheckListObject obj = new(objects[sel].name, Random.Range(1,objects[sel].maxCount+1), lines[c]);
            checkListObjects[c] = obj;

            objects.RemoveAt(sel);

            c++;
        }
    }


    private void CheckList_OnObjectStolen(StealableObject obj) 
    {
        for(int i = 0;i < lineCount;i++)
        {
            if (obj.name.Equals(checkListObjects[i].GetName()))
            {
                checkListObjects[i].ObjectHasStolen();
                if (PlayerWins())
                {
                    CheckMark.SetActive(true);
                    canFinish = true;
                }
                
            }
        }
    }

    bool PlayerWins()
    {
        bool isCompleted = true;
        for (int i = 0; i < lineCount; i++)
        {
            if (!checkListObjects[i].ObjectsAreCollected)
            {
                isCompleted = false;
                break;
            }
        }
        return isCompleted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if(isOpen)
            {
                rectTransform.DOMove(privatePos, 0.75f);
                isOpen = false;
            }
            else
            {
                rectTransform.DOMove(publicPos, 0.75f);
                isOpen = true;
            }
        }
    }

    void SetOffSet()
    {
        float ratiox = (float)Screen.height / 1920f;
        float ratioy = (float)Screen.width / 1080f;

        rectTransform = GetComponent<RectTransform>();


        privatePos = rectTransform.position + new Vector3(privateOffSet.x*ratiox,privateOffSet.y*ratioy,privateOffSet.z);
        publicPos = rectTransform.position + new Vector3(publicOffSet.x * ratiox, publicOffSet.y * ratioy, publicOffSet.z);

        rectTransform.position = privatePos;
    }


    private void OnDisable()
    {
        PlayerMovement.OnObjectStolen -= CheckList_OnObjectStolen;
    }

 
}
