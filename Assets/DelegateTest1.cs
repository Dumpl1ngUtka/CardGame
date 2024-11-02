using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest1 : MonoBehaviour
{
    private Func<int, int, string> createString = (a, b) => $"{a}{b}";
    private Func<SituationAnalyzer, float> matrix;


    private void Awake()
    {
        Debug.Log(matrix);
    }

    private float GetMetrix(SituationAnalyzer situationAnalyzer)
    {
        return 0;
    }
}
