using UnityEngine;
using System;
using Unity.VisualScripting;

public class Money : MonoBehaviour
{
    public long targetGold;
    public long targetJelatin;

    private double displayGold;
    private double displayJelatin;
    public static Money instance;

    void Awake()
    {
        instance = this;
    }

    void LateUpdate()
    {
        displayGold = LerpDouble(displayGold, targetGold, 0.1);
        if (Math.Abs(displayGold - targetGold) < 1.0)
            displayGold = targetGold;

        GameManager.instance.gold.text = Math.Round(displayGold).ToString("N0");

        displayJelatin = LerpDouble(displayJelatin, targetJelatin, 0.1);
        if (Math.Abs(displayJelatin - targetJelatin) < 1.0)
            displayJelatin = targetJelatin;

        GameManager.instance.jelatin.text = Math.Round(displayJelatin).ToString("N0");


    }

    double LerpDouble(double a, long b, double t)
    {
        return a + (b - a) * t;
    }
}