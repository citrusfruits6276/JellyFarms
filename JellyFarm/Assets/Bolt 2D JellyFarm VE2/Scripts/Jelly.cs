using System.Collections;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriter;
    public float speed;
    public float speedX;
    public float speedY;
    public int idleTime;
    bool isMoving;


    void Awake()
    {
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            isMoving = false;
            anim.SetBool("isWalk", false);
            idleTime = Random.Range(3, 6);
            yield return new WaitForSeconds(idleTime);

            isMoving = true;
            anim.SetBool("isWalk", true);
            speedX = Random.Range(-1f, 1f);
            speedY = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(2f);
        }

    }

    void FixedUpdate()
    {

        if (isMoving)
        {
            Vector3 move = new Vector3(speed * speedX * Time.deltaTime, speed * speedY * Time.deltaTime, 0);
            transform.position += move;
            spriter.flipX = move.x < 0;
        }

        Vector3 pos = transform.position;

        float minX = GameManager.instance.topLeft.position.x;
        float maxX = GameManager.instance.BottomRight.position.x;
        float minY = GameManager.instance.BottomRight.position.y;
        float maxY = GameManager.instance.topLeft.position.y;

        if (pos.x < minX || pos.x > maxX)
            speedX *= -1;


        if (pos.y < minY || pos.y > maxY)
            speedY *= -1;
    }
}
