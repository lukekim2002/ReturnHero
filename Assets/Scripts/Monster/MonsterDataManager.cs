using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : MonoBehaviour {

    #region PUBLIC VALUES

    public enum MONSTER { GATEKEEPER = 0, // Stage 0
        ZOMBIE, SKELETON, GHOUL, LICH, NECROMANCER // Stage 1
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
        monsterDataSet = CSVReader.Read("ReturnHero_Monster_DataSet");
    }

    public Dictionary<string, object> ThrowDataIntoContainer(int index)
    {
        return monsterDataSet[index];
    }

    #endregion
}
