using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterPropertySet {

    int Health { get; set; }
    float MovingSpeed { get; set; }

    int MeleeDamage { get; set; }
    float MeleeCoolDown { get; set; }
    Vector2 MeleeRange { get; set; }
    Vector2 MeleeBoxSize { get; set; }

    int Skill1Damage { get; set; }
    float Skill1CoolDown { get; set; }
    Vector2 Skill1Range { get; set; }
    Vector2 Skill1BoxSize { get; set; }

    int Skill2Damage { get; set; }
    float Skill2CoolDown { get; set; }
    Vector2 Skill2Range { get; set; }
    Vector2 Skill2BoxSize { get; set; }

    int Skill3Damage { get; set; }
    float Skill3CoolDown { get; set; }
    Vector2 Skill3Range { get; set; }
    Vector2 Skill3BoxSize { get; set; }

    int Skill4Damage { get; set; }
    float Skill4CoolDown { get; set; }
    Vector2 Skill4Range { get; set; }
    Vector2 Skill4BoxSize { get; set; }

}
