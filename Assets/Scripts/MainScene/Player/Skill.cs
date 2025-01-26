using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string skillName;
    public Sprite skillIcon;
    
    public void UseSkill()
    {
        // Use skill logic
        switch(skillName)
        {
            case "sk1":
                // Skill1 logic
                break;
            case "sk2":
                // Skill2 logic
                break;
        }
    }
}