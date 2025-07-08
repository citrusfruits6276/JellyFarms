using System;
using System.Collections;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriter;
    Collider2D coll;
    public float speed;
    public float speedX;
    public float speedY;
    public int idleTime;
    bool isMoving;
    public int jellyId;
    public int jellyLevel;
    public float exp;

    Coroutine stateRoutine;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    void Start()
    {
        stateRoutine = StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            isMoving = false;
            anim.SetBool("isWalk", false);
            idleTime = UnityEngine.Random.Range(3, 6);
            yield return new WaitForSeconds(idleTime);

            isMoving = true;
            anim.SetBool("isWalk", true);
            speedX = UnityEngine.Random.Range(-1f, 1f);
            speedY = UnityEngine.Random.Range(-1f, 1f);
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
        float maxX = GameManager.instance.bottomRight.position.x;
        float minY = GameManager.instance.bottomRight.position.y;
        float maxY = GameManager.instance.topLeft.position.y;

        if (pos.x < minX || pos.x > maxX)
            speedX *= -1;


        if (pos.y < minY || pos.y > maxY)
            speedY *= -1;
    }

    void OnMouseDown()
    {
        exp++;
        if (exp == 50 || exp == 250)
            GameManager.instance.ChangeAc(anim, ++jellyLevel);
        Money.instance.targetJelatin += (jellyId + 1) * jellyLevel;
        Money.instance.targetJelatin = Math.Min(Money.instance.targetJelatin, 99999999);
        Debug.Log("오브젝트가 클릭됨!");
        anim.SetTrigger("doTouch");
        isMoving = false;

        if (stateRoutine != null)
            StopCoroutine(stateRoutine);

        stateRoutine = StartCoroutine(StateLoop());
    }
}
