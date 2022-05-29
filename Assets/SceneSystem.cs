using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : SingletonPersistent<SceneSystem>
{
    public void LoadScene(int index, float time = 0f)
    {
        if (index == -1)
            index = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneCoroutine(index, time));
    }
    IEnumerator LoadSceneCoroutine(int index, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
    }
}
