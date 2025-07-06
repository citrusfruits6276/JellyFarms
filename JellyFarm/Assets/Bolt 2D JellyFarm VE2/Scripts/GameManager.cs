using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform topLeft;
    public Transform bottomRight;
    public TMP_Text gold;
    public TMP_Text jelatin;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
}
