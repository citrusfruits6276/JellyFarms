using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform topLeft;
    public Transform BottomRight;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
}
