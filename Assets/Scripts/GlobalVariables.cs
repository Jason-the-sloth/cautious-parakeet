using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName =nameof(GlobalVariables),menuName =nameof(GlobalVariables))]
public class GlobalVariables : ScriptableObject
{
    [Header("Player Prefab Set Up")]
    [Space(10)]
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 0.5f;
    public float shootingInterval = 1f;
    public float bulletForce = 300f;
    public float viewRadius = 6f;
    public  float viewAngle = 90f;
    public float maxHealth = 10f;
    public GameObject bullet;
    public GameObject stats;

    [Space(10)]
    [Header("Player Set Up")]
    [Space(10)]
    public GameObject playerTriangle;
    public Vector2 p1;
    public Vector2 p2;

    [Space(10)]
    [Header("Bullet Prefab Set Up")]
    [Space(10)]
    public float deathCount = 1000;
    public float bulletDamage = 1f;
    public float bulletCoefficientDamage = 1f;

    [Space(10)]
    [Header("Board Set UP")]
    [Space(10)]
    public float Width = 10;
    public float Height = 10;
    public float rectangleWidth = 10f;
    public float rectangleHeight = 10f;

    [Space(10)]
    [Header("Camera Controls Set UP")]
    public float cameraMoveSpeed = 10.0f;

    public static Uri BaseAddress = new Uri("https://localhost:7201");
}
