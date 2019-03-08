using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : MonoBehaviour {

    #region PUBLIC VALUES

    public enum MONSTER { GATEKEEPER = 0, // Stage 0
        ZOMBIE, SKELETON, GHOUL, LICH, NECROMANCER, // Stage 1
        COW, TIGER, GORILLA, EAGLE, MANTICORE, // Stage 2
        DARK_ELEMENTAL, LIGHT_ELEMENTAL, FIRE_ELEMENTAL, ICE_ELEMENTAL, ELEMENTAL_KING, // Stage 3
        RED_BABY_DRAGON, BLUE_BABY_DRAGON, RED_DRAGON, BLUE_DRAGON, // Stage 4
        FINALBOSS // Final Stage
    };

    public static MonsterDataManager instance = null;
    public List<Dictionary<string, object>> monsterDataSet;
    //public List<Dictionary<string, object>> colliderDataSet;

    #endregion

    #region MONOBEHVAVIOUR CALLBACKS

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
            // then destroy this. This enforces our singleton pattern, meaning there caan only one instance.
            Destroy(this);
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        ReadDataFromFile();
    }
    #endregion

    #region PUBLIC METHODS

    public void ReadDataFromFile()
    {
        monsterDataSet = CSVReader.Read("CSV/Monster/ReturnHero_Monster_DataSet");
        Debug.Log(monsterDataSet.Count);
    }

    public Dictionary<string, object> ThrowDataIntoContainer(int index)
    {
        return monsterDataSet[index];
    }

    #endregion
}
