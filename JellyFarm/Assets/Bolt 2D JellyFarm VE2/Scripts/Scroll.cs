using UnityEngine;
using UnityEngine.UIElements;

public class Scroll : MonoBehaviour
{
    public float scrollSpeed;
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * scrollSpeed;
        transform.position = pos;

        if (pos.x > 8)
        {
            pos.x = -16;
            transform.position = pos;
        }
    }


}
