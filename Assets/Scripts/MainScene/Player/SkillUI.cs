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
    public TextMeshProUGUI coolDownText;
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
            StartCoroutine(skillCooldown());
            isSkillEnable = false;
            skillImage.enabled = false;
            coolDownText.gameObject.SetActive(true); // 显示冷却时间文本
        }
    }

    public IEnumerator skillCooldown()
    {
        float coolDownTime = 5f;
        float coolDownTimer = coolDownTime;

        while (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;

            coolDownImage.fillAmount = coolDownTimer / coolDownTime;

            coolDownText.text = Mathf.Ceil(coolDownTimer).ToString() + "s";

            yield return null;
        }

        skillImage.enabled = true;
        coolDownText.gameObject.SetActive(false);
    }
}