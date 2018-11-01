using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGeneralManager : MonoBehaviour {

    // public instance of GGM which allows it to be accessed by any other scripts.
    public static GameGeneralManager instance = null;
    public int curFloor = 1;

    private int UISceneNum = 16;

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

        Application.targetFrameRate = 60;

        curFloor = 1;
        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //print("Hi. This is GameGeneralManager.");
        //curFloor = SceneManager.GetActiveScene().buildIndex;
        print("Current Floor : " + curFloor);
        SceneManager.LoadScene(curFloor, LoadSceneMode.Additive);
        SceneManager.LoadScene(UISceneNum, LoadSceneMode.Additive);
    }

    /// <summary>
    /// This function checks whether there is a wall in front of the character.
    /// Return true if there is, else false.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public Vector2 IsWallInFrontOfCharacter(Vector2 position, Vector2 direction, float distance)
    {
        RaycastHit2D hitwall;
        Vector2 hitPoint = Vector2.zero;

        hitwall = Physics2D.Raycast(position, direction * distance, distance, 1 << LayerMask.NameToLayer("Wall"));

        if (hitwall.transform != null)
        {
            hitPoint = hitwall.point;
        }
        else
        {
            hitPoint = Vector2.zero;
        }

        return hitPoint;
    }

    
}
