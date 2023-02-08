using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Pedestrian : MonoBehaviour
{
    public int id = -1;
    public List<float[]> Trajectory = new List<float[]>();
    public bool Finished = false;
    public Vector3 initPoisiton;
    public GameObject tracePrefab;

    private int _positions = 0;
    private LineRenderer _trace;

    /// <summary>
    /// sets a new poition of the pedestrian
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void SetPosition(float x, float z)
    {
        if (transform.position != new Vector3(x, 0.01f, z))
        {
            gameObject.transform.position = new Vector3(x, 0.01f, z);
            
        }

    }

}
