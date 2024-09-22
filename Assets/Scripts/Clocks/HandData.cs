using System;
using UnityEngine;

[Serializable]
public class HandData
{
    [field:SerializeField] public Transform Hand { get; private set; }
    [field:SerializeField] public float AnimationDuration { get; private set; }
}

