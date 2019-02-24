using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 20f;
    [SerializeField] private Transform beerCollection;
    [SerializeField] private GameObject beerPrefab;
    
    private float startTime;
    private float endTime;
    private float interval;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;

    private Vector3 ballPosition;
    private Vector3 beerPosition;

    private bool hasPlayed = false;
    private Rigidbody ball;
    private IEnumerator coroutine;

    private void Awake()
    {
        ball = GetComponent<Rigidbody>();
        ballPosition = transform.position;
        beerPosition = beerCollection.position;
    }

    void Update()
    {
        if (Input.touchCount > 0  && hasPlayed == false)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = Input.GetTouch(0).position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = Input.GetTouch(0).position;

                interval = endTime - startTime;
                direction = endPos - startPos;

                Vector3 ballForce = new Vector3((direction.x / interval) * force * Time.deltaTime, 0, (direction.y / interval) * force * Time.deltaTime);

                ball.AddForce(ballForce);
                hasPlayed = true;
            }
        }

        if(hasPlayed == true)
        {
            if (coroutine != null) return;
            coroutine = ResetGame(5);
            StartCoroutine(coroutine);
        }

    }

    IEnumerator ResetGame(float time)
    {
        yield return new WaitForSeconds(time);

        ResetBall();
        ResetBeerCollection();
        hasPlayed = false;
    }

    void ResetBeerCollection(){
        Destroy(beerCollection);
        Instantiate(beerPrefab,beerPosition,Quaternion.identity);
    }

    void ResetBall(){
        transform.position = ballPosition;
    }

}
