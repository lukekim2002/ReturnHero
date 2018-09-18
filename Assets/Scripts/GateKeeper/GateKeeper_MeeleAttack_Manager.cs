using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper_MeeleAttack_Manager : MonoBehaviour {

    #region PRIVATE
    private Vector2 _gateKeeperPos;
    private Vector2 _gateKeeperSkillDirection = new Vector2(0, -1);
    #endregion

    private void GateKeeper_MeeleAttack_Excute()
    {
        _gateKeeperPos = this.transform.position;
        _gateKeeperPos.x += _gateKeeperSkillDirection.x * 0.16f;
        _gateKeeperPos.y += _gateKeeperSkillDirection.y * 0.16f;

        this.transform.position = _gateKeeperPos;
    }
}
