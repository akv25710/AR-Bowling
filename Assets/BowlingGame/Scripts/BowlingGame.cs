using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject beerCollection;

    private void Awake()
    {
        beerCollection.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(WaitActive(1));
    }

    IEnumerator WaitActive(float time)
    {
        yield return new WaitForSeconds(time);
            
        beerCollection.SetActive(true);
    }
}
