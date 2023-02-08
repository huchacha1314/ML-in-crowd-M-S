using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Manager : MonoBehaviour
{
    private string[] _files;
    private string _scenarioFile;
    private string _trajectoryFile;
    private ScenarioJSONClasses.Root _root;
     public float width;
    public float height;
    private bool _running = false;
    private List<Pedestrian> _pedestrians = new List<Pedestrian>();
    private int _pedestrianID = 1;

    public GameObject groundPrefab;
    public GameObject obstaclePrefab;
    public GameObject sourcePrefab;
    public GameObject targetPrefab;
    public GameObject pedestrianPrefab;
    public PedestrianManager pedsManager;
    public TextMeshProUGUI folderPath;

    /// <summary>
    /// transfomrs the x value of the vadere output file to local coordinates in unity
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public float TransformX(double val)
    {
        return (float)(val - width / 2) * 10;
    }
    
    /// <summary>
    /// transfomrs and returns the y value of the vadere output file to local coordinates in unity
    /// </summary>
    /// <param name="val"></param> 
    /// <returns></returns>
    public float TransformY(double val)
    {
        return (float)(val - height / 2) * 10;
    }
    
    /// <summary>
    /// places the obstacles in the scene
    /// </summary>
    /// <param name="obstacles"></param>list of obstacles specified in the JSON file from vadere
    private void GenerateObstacles(List<ScenarioJSONClasses.Obstacle> obstacles)
    {
        foreach (var obstacle in obstacles)
        {
            Object obs = Instantiate(obstaclePrefab, new Vector3(TransformX((obstacle.shape.x)), 0, TransformY(obstacle.shape.y)), Quaternion.identity);
            obs.GameObject().transform.localScale =
                new Vector3((float) obstacle.shape.width * 10, 10, (float) obstacle.shape.height * 10);
        }
    }

    /// <summary>
    /// places the sources and pedetrians in the scene
    /// </summary>
    /// <param name="sources"></param> list of sources specified in the JSON file from vadere
    private void GenerateSources(List<ScenarioJSONClasses.Source> sources)
    {
        foreach (var source in sources)
        {
            Object src = Instantiate(sourcePrefab, new Vector3(TransformX((source.shape.x)), 0, TransformY(source.shape.y)), Quaternion.identity);
            src.GameObject().transform.localScale =
                new Vector3((float) source.shape.width * 10, 10, (float) source.shape.height * 10);
            for (int i = 0; i < source.spawner.eventElementCount; i++)
            {
                Object ped = Instantiate(pedestrianPrefab,
                    new Vector3(TransformX((source.shape.x)), 0, TransformY(source.shape.y)), Quaternion.identity);
                ped.GameObject().GetComponent<Pedestrian>().id = _pedestrianID++;
                ped.GameObject().GetComponent<Pedestrian>().initPoisiton = ped.GameObject().transform.position;
                _pedestrians.Add(ped.GameObject().GetComponent<Pedestrian>());
            }
        }
    }
    
    /// <summary>
    /// places the targets in the scene
    /// </summary>
    /// <param name="targets"></param> list of targets specified in the JSON file from vadere
    private void GenerateTargets(List<ScenarioJSONClasses.Target> targets)
    {
        foreach (var target in targets)
        {
            Object tar = Instantiate(targetPrefab, new Vector3(TransformX((target.shape.x)), 0, TransformY(target.shape.y)), Quaternion.identity);
            tar.GameObject().transform.localScale =
                new Vector3((float) target.shape.width * 10, 10, (float) target.shape.height * 10);
        }
    }
    
    /// <summary>
    /// generates the obstacles, sources and targets for the scneario
    /// </summary>
    private void GenerateScenario()
    {
        Object ground = Instantiate(groundPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        height = (float) _root.scenario.topography.attributes.bounds.width;
        width = (float) _root.scenario.topography.attributes.bounds.height;
        ground.GameObject().transform.localScale = new Vector3(width, 1, height);
        
        GenerateObstacles(_root.scenario.topography.obstacles);
        GenerateSources(_root.scenario.topography.sources);
        GenerateTargets(_root.scenario.topography.targets);
    }
    

    /// <summary>
    /// takes the two output files .scenario and .traj from the vadere output and begins generating the scenario
    /// </summary>
    public void OpenFolder()

    {
        string path_neu = @folderPath.text.Replace("\\", "/");
        path_neu = Regex.Replace(path_neu, @"[^\u0000-\u007F]+", string.Empty);
        //string path = EditorUtility.OpenFolderPanel("Select scneario output folder", "", "");
        //Debug.Log(path.Equals(path_neu));
        //Debug.Log(path_neu);
        //Debug.Log(path);
        _files = Directory.GetFiles(path_neu);

        foreach (var file in _files)
        {
            if (file.EndsWith(".scenario"))
                _scenarioFile = file;
            if (file.EndsWith(".traj"))
                _trajectoryFile = file;
        }

        if (_scenarioFile == null || _trajectoryFile == null)
        {
            Debug.Log("wrong folder input");
        }
        
        string json = File.ReadAllText(_scenarioFile);
        
        _root = JsonUtility.FromJson<ScenarioJSONClasses.Root>(json);
        
        GenerateScenario();
        pedsManager.HandlePedestrians(_pedestrians, _trajectoryFile, _root.scenario.attributesSimulation.realTimeSimTimeRatio);
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
