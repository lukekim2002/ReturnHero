using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGeneralManager : MonoBehaviour {


    #region PUBLIC STATIC VARAIBLES
    public static HeroGeneralManager instance = null;


    #region General

    public GameObject heroObject;

    public decimal health = 3;

    #endregion


    #region Physics
    public GameObject attackCollider;
    public BoxCollider2D atkCollider;
    public List<Dictionary<string, object>> heroAttackCollierSet;
    public Vector2 colliderSize = new Vector2(0.3f, 0.2f);
    public float colliderDiagonalLength = 0.18f;


    #endregion

    #endregion

    private void Awake()
    {
        // Check if instance doesn't exist
        if (instance == null)
        {
            instance = this;
        }

        // If instance already exists and it's not this
        else if (instance != this)
        {
            // then destroy this. This enforces our singleton pattern, meaning there caan only one instance of GGM.
            Destroy(this);
        }

        heroObject = GameObject.FindGameObjectWithTag("Player");
        heroAttackCollierSet = CSVReader.Read("HeroAttackCollider");

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        atkCollider = attackCollider.GetComponent<BoxCollider2D>();
        SetAttackColliderInactive();
    }

    #region Public Methods

    public void SetAttackColliderActive()
    {
        attackCollider.SetActive(true);
        atkCollider.size = new Vector2(1, 1);
        atkCollider.offset = new Vector2(0, 0);
    }

    public void SetAttackColliderInactive()
    {
        atkCollider.size = new Vector2(1, 1);
        atkCollider.offset = new Vector2(0, 0);
        attackCollider.SetActive(false);
    }

    #endregion

}
