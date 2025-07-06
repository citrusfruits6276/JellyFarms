using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RuntimeAnimatorController[] levelAc;
    public Transform topLeft;
    public Transform bottomRight;
    public TMP_Text gold;
    public TMP_Text jelatin;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = levelAc[level - 1];
    }
}
