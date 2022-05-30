using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinLoseManager : MonoBehaviour
{
    public static UnityEvent OnCombatEnd = new UnityEvent();
    public static UnityEvent OnWinEvent = new UnityEvent();
    public static UnityEvent OnLoseEvent = new UnityEvent();

    public GameObject[] destroyOnCombatEnd;
    public DicePicking dicePicking;

    private void Awake()
    {
        OnWinEvent.AddListener(Win);
        OnLoseEvent.AddListener(Lose);
    }

    public void Win()
    {
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        AudioManager.Instance.Play("Win");
        CombatEnd();
        yield return new WaitForSeconds(1.5f);
        dicePicking.gameObject.SetActive(true);
    }
    public void Lose()
    {
        StartCoroutine(LoseCoroutine());
    }
    IEnumerator LoseCoroutine()
    {
        AudioManager.Instance.Play("Lose");
        CombatEnd();

        yield return null;

        //Spouse Scene
        SceneSystem.Instance.LoadScene(2, 2.2f);
    }

    void CombatEnd()
    {
        OnCombatEnd.Invoke();
        for (int i = 0; i < destroyOnCombatEnd.Length; i++)
        {
            Destroy(destroyOnCombatEnd[i]);
        }
    }
}
