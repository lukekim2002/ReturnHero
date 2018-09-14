using UnityEngine;
using System.Collections;

public class Skill_MR_EffectManager : MonoBehaviour
{
    #region PRIVATE
    private Animator _skillMrAnimator;
    private int _transparencyCount = 0;

    private SpriteRenderer spr;
    private BoxCollider2D collider;


    private Vector2 colliderSize;
    private Vector2 colliderOffset;
    private int i = 0;

    private int transparencyCount = 1;
    #endregion

    #region PUBLIC


    public enum ATTACKDIRECTION { NONE, UP = 24, DOWN = 25, LEFT = 26, RIGHT = 27  };
    static public ATTACKDIRECTION attackDirection = ATTACKDIRECTION.NONE;
    public Transform heroPos;
    // MR 이펙트 애니메이션 타이밍
    static public bool isSkillMr = false;
    #endregion

    private void Start()
    {
        _skillMrAnimator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkillMr)
        {
            if (attackDirection == ATTACKDIRECTION.UP)
            {
                _skillMrAnimator.SetTrigger("SkillMrUp");
            }
            else if (attackDirection == ATTACKDIRECTION.LEFT)
            {
                _skillMrAnimator.SetTrigger("SkillMrLeft");
            }
            else if (attackDirection == ATTACKDIRECTION.RIGHT)
            {
                _skillMrAnimator.SetTrigger("SkillMrRight");
            }
            else if (attackDirection == ATTACKDIRECTION.DOWN)
            {
                _skillMrAnimator.SetTrigger("SkillMrDown");
            }
            /*
            collider.enabled = true;
            skillMrAnimator.enabled = true;
            spr.enabled = true;
            */
            this.GetComponent<Renderer>().enabled = true;
            collider.enabled = true;
        }
    }

    private void Skill_MR_Effect_Excute()
    {
        Vector2 skillPos = transform.position;
        i = (int)attackDirection;

        if (attackDirection == ATTACKDIRECTION.UP)
        {
            
            skillPos.y = skillPos.y + 0.2f;
            this.transform.position = skillPos;
        }
        else if (attackDirection == ATTACKDIRECTION.LEFT)
        {
            skillPos.x = skillPos.x - 0.2f;
            this.transform.position = skillPos;
        }
        else if (attackDirection == ATTACKDIRECTION.RIGHT)
        {
            skillPos.x = skillPos.x + 0.2f;
            this.transform.position = skillPos;
        }
        else if (attackDirection == ATTACKDIRECTION.DOWN)
        {
            skillPos.y = skillPos.y - 0.2f;
            this.transform.position = skillPos;
        }

        colliderSize = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Height"]);
        colliderOffset = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Height"]);
        collider.size = colliderSize;
        collider.offset = colliderOffset;
    }

    private void Skill_MR_Effect_End()
    {

        collider.enabled = false;
        attackDirection = ATTACKDIRECTION.NONE;
        isSkillMr = false;
        this.GetComponent<Renderer>().enabled = false;
    }

    // MR 스킬 이펙트의 투명도를 낮춘다.
    private void Skill_MR_Effect_ChangeAlpha()
    {
        Color color = spr.color;

        // _transparencyCount가 3이면 원래대로 초기화
        if (_transparencyCount == 3)
        {
            color.a = 1.0f;
            spr.color = color;
            _transparencyCount = 0;
        }
        // 3이 아니라면 투명도 낮춤
        else
        {
            color.a -= 0.25f;
            spr.color = color;
            _transparencyCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            this.gameObject.SetActive(false);
            /*
            collider.enabled = false;
            skillMrAnimator.enabled = false;
            spr.enabled = false;
            */
        }
    }

}
