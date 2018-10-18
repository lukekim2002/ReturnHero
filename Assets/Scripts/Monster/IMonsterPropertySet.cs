using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterPropertySet {

    int Health { get; set; }
    float MovingSpeed { get; set; }

    int MeleeDamage { get; set; }
    float MeleeCoolDown { get; set; }
    Vector2 MeleeRange { get; set; }
    Vector2 MeleeBoxSizeUp { get; set; }
    Vector2 MeleeBoxSizeDown { get; set; }
    Vector2 MeleeBoxSizeLeft { get; set; }
    Vector2 MeleeBoxSizeRight { get; set; }

    int Skill1Damage { get; set; }
    float Skill1CoolDown { get; set; }
    Vector2 Skill1Range { get; set; }
    Vector2 Skill1BoxSizeUp { get; set; }
    Vector2 Skill1BoxSizeDown { get; set; }
    Vector2 Skill1BoxSizeLeft { get; set; }
    Vector2 Skill1BoxSizeRight { get; set; }

    int Skill2Damage { get; set; }
    float Skill2CoolDown { get; set; }
    Vector2 Skill2Range { get; set; }
    Vector2 Skill2BoxSizeUp { get; set; }
    Vector2 Skill2BoxSizeDown { get; set; }
    Vector2 Skill2BoxSizeLeft { get; set; }
    Vector2 Skill2BoxSizeRight { get; set; }

    int Skill3Damage { get; set; }
    float Skill3CoolDown { get; set; }
    Vector2 Skill3Range { get; set; }
    Vector2 Skill3BoxSizeUp { get; set; }
    Vector2 Skill3BoxSizeDown { get; set; }
    Vector2 Skill3BoxSizeLeft { get; set; }
    Vector2 Skill3BoxSizeRight { get; set; }

    int Skill4Damage { get; set; }
    float Skill4CoolDown { get; set; }
    Vector2 Skill4Range { get; set; }
    Vector2 Skill4BoxSizeUp { get; set; }
    Vector2 Skill4BoxSizeDown { get; set; }
    Vector2 Skill4BoxSizeLeft { get; set; }
    Vector2 Skill4BoxSizeRight { get; set; }

}
