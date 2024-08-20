using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goarrow : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] GameObject Arrow;

    private bool isCoroutineRunning;

    private void Awake()
    {
        isCoroutineRunning = false;
    }

    public void flashArrow()
    {
        if (!isCoroutineRunning) StartCoroutine(Flash(delay, Arrow));
    }

    IEnumerator Flash (float delay, GameObject Arrow)
    {
        isCoroutineRunning = true;
        Arrow.SetActive (true);
        yield return new WaitForSeconds (delay);
        Arrow.SetActive(false);
        yield return new WaitForSeconds(delay);
        Arrow.SetActive(true);
        yield return new WaitForSeconds(delay);
        Arrow.SetActive(false);
        yield return new WaitForSeconds(delay);
        Arrow.SetActive(true);
        yield return new WaitForSeconds(delay);
        Arrow.SetActive(false);
        yield return new WaitForSeconds(delay);
        isCoroutineRunning = false;
    }
}
