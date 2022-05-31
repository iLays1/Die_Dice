using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSceneOnButton : MonoBehaviour
{
    [SerializeField] int sceneIndex;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SceneSystem.Instance.LoadScene(sceneIndex,0.2f);
    }
}
