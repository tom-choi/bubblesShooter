using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public bool isSkillEnable = true;
    public Player player;
    public TextMeshProUGUI skillNameUI;

    public Image coolDownImage;
    public Image skillImage;

    [SerializeField]
    private Skill skillCache;
    public int skillIndex;
    void Start()
    {
        
    }

    public IEnumerator ReadPlayerData()
    {
        yield return new WaitUntil(() => player.isPlayerDataLoaded);
        skillCache = player.skills[skillIndex];
        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        skillNameUI.text = skillCache.skillName;
        skillImage.sprite = skillCache.skillIcon;
    }

    public void UseSkill()
    {
        if (isSkillEnable)
        {

            StartCoroutine(skillCooddown());
            isSkillEnable = false;
            skillImage.enabled = false;
        }
    }

    public IEnumerator skillCooddown()
    {
        float coolDownTime = 5f;
        float coolDownTimer = coolDownTime;
        while (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
            coolDownImage.fillAmount = coolDownTimer / coolDownTime;
            yield return null;
        }
        skillImage.enabled = true;
    }

}
