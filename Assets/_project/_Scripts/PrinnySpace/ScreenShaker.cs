using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public static ScreenShaker main;

    private void Start()
    {
        main = this;
    }

    public void Shake(float dur, float mag)
    {
        StartCoroutine(ShakeCoroutine(dur,mag));
    }

    IEnumerator ShakeCoroutine(float dur, float mag)
    {
        Vector3 oPos = transform.localPosition;

        float timer = 0f;

        while(timer < dur)
        {
            var x = Random.Range(-1f,1f) * mag;
            var y = Random.Range(-1f,1f) * mag;

            transform.localPosition = oPos + new Vector3(x,y, 0);

            timer += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = oPos;
    }
}
