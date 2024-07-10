using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
public class ObjectTransformInfo 
{
    public string name;
    public Vector3 position;
    public Vector3 localPosition;
    public Vector3 up;
    public Vector3 right;
    public Vector3 forward;
    public Quaternion localRotation;
    public Quaternion rotation; 

}

//[Serializable]
//public class SerializableVector3
//{
//    public float x;
//    public float y;
//    public float z;

//    public SerializableVector3(Vector3 vector)
//    {
//        x = vector.x;
//        y = vector.y;
//        z = vector.z;
//    }

//    public Vector3 ToVector3()
//    {
//        return new Vector3(x, y, z);
//    }
//}
