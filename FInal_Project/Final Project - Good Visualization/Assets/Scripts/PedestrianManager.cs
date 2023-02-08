using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class PedestrianManager : MonoBehaviour
{
    private bool _running = false;
    private List<Pedestrian> _pedestrians;
    private Stopwatch _timer = new Stopwatch();
    private double _timeStepLength;
    private int _timeStep;

    public Manager manager;
    private List<float[]> _steps;
    

    /// <summary>
    /// handles the movement of all pedestrians every frame with the help of a timer
    /// </summary>
    void Update()
    {
        if (_running)
        {
            _timer.Stop();
            _timer.Start();
            double currentTime = _timer.Elapsed.TotalSeconds;
            _timeStep = Convert.ToInt32(currentTime / _timeStepLength);
            foreach (float[] step in _steps)
            {
                if (currentTime > step[1])
                {
                    _pedestrians.Find(i => i.id.Equals((int) step[0])).SetPosition(manager.TransformX(step[5]), manager.TransformY(step[6]));
                }
            }
        }
    }
    
    /// <summary>
    /// generates the pedestrian at their sources
    /// </summary>
    /// <param name="peds"></param> list of pedestrians
    /// <param name="filePath"></param> filepath to the .traj file
    /// <param name="timeStep"></param> 
    public void HandlePedestrians(List<Pedestrian> peds, string filePath, double timeStep)
    {
        _pedestrians = peds;
        _timeStepLength = timeStep;
        
         _steps =
            File.ReadLines(filePath)
                .Skip(1)
                .Select(line => line.Split(' ').Select(s => float.Parse(s,CultureInfo.InvariantCulture.NumberFormat)).ToArray())
                .ToList();
    }

    /// <summary>
    /// starts the timer when the playButton is pressed
    /// </summary>
    public void HitPlayButton()
    {
        _timer.Start();
        _running = true;
    }

    /// <summary>
    /// pauses the timer when the pauseButton is pressed
    /// </summary>
    public void HitPauseButton()
    {
        _timer.Stop();
        _running = false;
    }

    /// <summary>
    /// resets the pedestrian to their initial position and resets the timer
    /// </summary>
    public void Reset()
    {
        _timer.Stop();
        _timer.Reset();

        foreach (Pedestrian ped in _pedestrians)
        {
            
            ped.transform.position = ped.initPoisiton;
            ped.Finished = false;
            ped.GetComponent<TrailRenderer>().Clear();
        }
    }

    /// <summary>
    /// handles the activation of trajectories
    /// </summary>
    public void ActivateTrails()
    {
        foreach (Pedestrian pedestrian in _pedestrians)
        {
            pedestrian.GetComponent<TrailRenderer>().emitting = true;
        }
    }
    
    /// <summary>
    /// handles the deactivation of trajectories
    /// </summary>
    public void DeactivateTrails()
    {
        foreach (Pedestrian pedestrian in _pedestrians)
        {
            pedestrian.GetComponent<TrailRenderer>().emitting = false;
        }
    }
}
