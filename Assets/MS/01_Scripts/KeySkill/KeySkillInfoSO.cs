using UnityEngine;

[CreateAssetMenu(menuName = "SO/KeySkill/KeySkillInfo")]
public class KeySkillInfoSO : ScriptableObject
{
    
    public string skillName;
    [HideInInspector]public KeySkill skill;

    private void OnValidate()
    {
        if (skillName == null) return;
        skill.skillName = skillName;
    }
} 
