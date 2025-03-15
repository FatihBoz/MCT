using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerSkillUI : MonoBehaviour
{

    [SerializeField] private Image skillIcon;

    private float maxTime;
    private float currentTime;



    private void Start()
    {
        maxTime = PlayerSkill.instance.cooldown;
    }

    private void Update()
    {
        if (!PlayerSkill.instance.skillCanBeCasted)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= maxTime)
            {
                PlayerSkill.instance.skillCanBeCasted = true;
                currentTime = 0;
                return;
            }

            skillIcon.fillAmount = currentTime / maxTime;
        }
    }

    private void OnEnable()
    {
        PlayerSkill.OnSkillCasted += PlayerSkillUI_OnSkillCasted;
    }

    private void PlayerSkillUI_OnSkillCasted()
    {
        PlayerSkill.instance.skillCanBeCasted = false;
    }

    private void OnDisable()
    {
        PlayerSkill.OnSkillCasted -= PlayerSkillUI_OnSkillCasted;
    }
}
