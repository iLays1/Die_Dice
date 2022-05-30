using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : SingletonPersistent<SceneSystem>
{
    public EncounterData[] encounters;
    int EncounterIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        PlayerDataSystem.Instance.nextEncounter = encounters[EncounterIndex];
    }
 

    public void NextLevel()
    {
        EncounterIndex++;
        if(EncounterIndex >= encounters.Length)
        {
            Debug.Log("FUCK");
            return;
        }

        PlayerDataSystem.Instance.nextEncounter = encounters[EncounterIndex];
        LoadScene(1);
    }

    public void LoadScene(int index, float time = 0f)
    {
        if (index == -1)
            index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0)
        {
            this.EncounterIndex = 0;
            PlayerDataSystem.Instance.nextEncounter = encounters[EncounterIndex];
        }

        StartCoroutine(LoadSceneCoroutine(index, time));
    }
    IEnumerator LoadSceneCoroutine(int index, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
    }
}
