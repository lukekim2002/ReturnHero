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
    private Vector2 tempHeroMRObjectPos;
    private Vector2 _direction;
    private int i = 0;

    private Transform heroObject;

    private int heroMRObjectMovePx = 0;
    #endregion

    #region PUBLIC
    public enum ATTACKDIRECTION { NONE, UP = 24, DOWN = 25, LEFT = 26, RIGHT = 27  };
    static public ATTACKDIRECTION attackDirection = ATTACKDIRECTION.NONE;
    public Transform heroPos;
    // MR 이펙트 애니메이션 타이밍
    public static bool isSkillMr = false;
    #endregion

    private void OnEnable()
    {
        _skillMrAnimator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();

        // MR Effect의 부모 오브젝트를 저장
        heroObject = this.transform.parent;
        // 부모 오브젝트 좌표를 MR Effect에 설정
        this.transform.position = heroObject.position;
        // MR Effect의 부모 오브젝트를 MR Effect의 부모오브젝트의 부모오브젝트로 바꾼다. (Skeleton에서 해제한다.)
        this.transform.parent = this.transform.parent.parent;

        _direction = heroObject.GetComponent<HeroController>().direction;

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

        
            StartCoroutine(MoveHeroMREffect());
        }

    }

    IEnumerator MoveHeroMREffect()
    {
        while (heroMRObjectMovePx < 12)
        {
            tempHeroMRObjectPos = this.transform.position;
            tempHeroMRObjectPos += _direction * 0.2f;

            this.transform.position = tempHeroMRObjectPos;
            print(heroMRObjectMovePx);
            heroMRObjectMovePx++;

            //colliderSize = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Size_Height"]);
            //colliderOffset = new Vector2((float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Width"], (float)HeroGeneralManager.instance.heroAttackCollierSet[i]["Offset_Height"]);
            //collider.size = colliderSize;
            //collider.offset = colliderOffset;

            yield return new WaitForSeconds((float)1/27);
        }
    }

    private void Skill_MR_Effect_End()
    {
        // 콜라이더 모습 숨김
        collider.enabled = false;
        // 방향 없음
        attackDirection = ATTACKDIRECTION.NONE;
        // Skill MR 발동 해제
        isSkillMr = false;
        // 다시 자식 오브젝트로 기어들어감
        transform.parent = heroObject;
        // 움직일 px 카운트를 0으로 초기화
        heroMRObjectMovePx = 0;
        // 그냥 꺼버려
        gameObject.SetActive(false);
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
