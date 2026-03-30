using System.Collections.Generic;
using UnityEngine;

public struct KeySkill
{
    public string skillName;
    public bool isHave;
}

public class SkillFactory : MonoBehaviour, IAgentComponent
{
    private Dictionary<string, KeySkill> _haveKeySkill = new();

    public List<KeySkillInfoSO> AllSkillList = new List<KeySkillInfoSO>();
    public List<KeySkill> currentSkillList = new List<KeySkill>();


    private Player _player;

    public void Initialize(Player agent)
    {
        _player = agent;

        foreach (KeySkillInfoSO item in AllSkillList)
        {
            _haveKeySkill.Add(item.skillName, item.skill);
        }

        
    }

    public KeySkill GetSkill(string skillName) => _haveKeySkill[name];

    public void Initialize(Agent agent) => Initialize(agent as Player);

    public void AddSkill(string skillName)
    {
        KeySkill skill = _haveKeySkill[skillName];
        skill.isHave = true;
        _haveKeySkill[skillName] = skill;

        Debug.Log(_haveKeySkill[skillName].isHave + _haveKeySkill[skillName].skillName);
    }

    public void ChangeSkill(string currentSkillName, string changeSkillName)
    {
        foreach (KeySkill item in currentSkillList)
        {
            if (item.skillName == currentSkillName)
            {
                currentSkillList.Remove(item);
                currentSkillList.Add(_haveKeySkill[changeSkillName]);
            }
        }
    }
}
