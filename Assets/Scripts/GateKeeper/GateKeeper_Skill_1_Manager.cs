﻿using UnityEngine;
using System.Collections;

public class GateKeeper_Skill_1_Manager : MonoBehaviour
{
    #region PRIVATE
    private Vector2 _gateKeeperPos;
    private Vector2 _gateKeeperSkillDirection = new Vector2(0, -1);
    #endregion

    private void GateKeeper_Skill_1_Excute1()
    {
        _gateKeeperPos = this.transform.position;
        _gateKeeperPos.x += _gateKeeperSkillDirection.x * 0.32f;
        _gateKeeperPos.y += _gateKeeperSkillDirection.y * 0.32f;

        this.transform.position = _gateKeeperPos;
    }

    private void GateKeeper_Skill_1_Excute2()
    {
        _gateKeeperPos = this.transform.position;
        _gateKeeperPos.x += _gateKeeperSkillDirection.x * 4.18f;
        _gateKeeperPos.y += _gateKeeperSkillDirection.y * 4.18f;

        this.transform.position = _gateKeeperPos;
    }
}
