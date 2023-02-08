using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
/// <summary>
/// in this file all the generated classes are stored according to the json output file of vadere. 
/// </summary>
public class ScenarioJSONClasses : MonoBehaviour
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [System.Serializable]
    public class Absorber
    {
        public bool enabled;
        public double deletionDistance;
    }

    [System.Serializable]
    public class Attributes
    {
        public int pedestrianOverlapProcessorId;
        public Bounds bounds;
        public double boundingBoxWidth;
        public bool bounded;
        public object referenceCoordinateSystem;
    }

    [System.Serializable]
    public class AttributesCar
    {
        public int id;
        public Shape shape;
        public bool visible;
        public double radius;
        public bool densityDependentSpeed;
        public double speedDistributionMean;
        public double speedDistributionStandardDeviation;
        public double minimumSpeed;
        public double maximumSpeed;
        public double acceleration;
        public int footstepHistorySize;
        public double searchRadius;
        public double walkingDirectionSameIfAngleLessOrEqual;
        public string walkingDirectionCalculation;
        public double length;
        public double width;
        public Direction direction;
    }

    [System.Serializable]
    public class AttributesModel
    {
        [JsonProperty("org.vadere.state.attributes.models.AttributesOSM")]
        public OrgVadereStateAttributesModelsAttributesOSM orgvaderestateattributesmodelsAttributesOSM;

        [JsonProperty("org.vadere.state.attributes.models.AttributesPotentialCompactSoftshell")]
        public OrgVadereStateAttributesModelsAttributesPotentialCompactSoftshell orgvaderestateattributesmodelsAttributesPotentialCompactSoftshell;

        [JsonProperty("org.vadere.state.attributes.models.AttributesFloorField")]
        public OrgVadereStateAttributesModelsAttributesFloorField orgvaderestateattributesmodelsAttributesFloorField;

        [JsonProperty("org.vadere.state.attributes.models.psychology.perception.AttributesSimplePerceptionModel")]
        public OrgVadereStateAttributesModelsPsychologyPerceptionAttributesSimplePerceptionModel orgvaderestateattributesmodelspsychologyperceptionAttributesSimplePerceptionModel;

        [JsonProperty("org.vadere.state.attributes.models.psychology.cognition.AttributesSimpleCognitionModel")]
        public OrgVadereStateAttributesModelsPsychologyCognitionAttributesSimpleCognitionModel orgvaderestateattributesmodelspsychologycognitionAttributesSimpleCognitionModel;
    }

    [System.Serializable]
    public class AttributesPedestrian
    {
        public Shape shape;
        public bool visible;
        public double radius;
        public bool densityDependentSpeed;
        public double speedDistributionMean;
        public double speedDistributionStandardDeviation;
        public double minimumSpeed;
        public double maximumSpeed;
        public double acceleration;
        public int footstepHistorySize;
        public double searchRadius;
        public double walkingDirectionSameIfAngleLessOrEqual;
        public string walkingDirectionCalculation;
    }

    [System.Serializable]
    public class AttributesPsychology
    {
        public bool usePsychologyLayer;
        public PsychologyLayer psychologyLayer;
    }

    [System.Serializable]
    public class AttributesSimulation
    {
        public double finishTime;
        public double simTimeStepLength;
        public double realTimeSimTimeRatio;
        public bool writeSimulationData;
        public bool visualizationEnabled;
        public bool printFPS;
        public int digitsPerCoordinate;
        public bool useFixedSeed;
        public long fixedSeed;
        public long simulationSeed;
    }

    [System.Serializable]
    public class Bounds
    {
        public double x;
        public double y;
        public double width;
        public double height;
    }

    [System.Serializable]
    public class Direction
    {
        public double x;
        public double y;
    }

    [System.Serializable]
    public class Distribution
    {
        public string type;
        public double updateFrequency;
    }

    [System.Serializable]
    public class File
    {
        public string type;
        public string filename;
        public List<int> processors;
    }

    [System.Serializable]
    public class Obstacle
    {
        public int id;
        public Shape shape;
        public bool visible;
    }

    [System.Serializable]
    public class OrgVadereStateAttributesModelsAttributesFloorField
    {
        public string createMethod;
        public double potentialFieldResolution;
        public double obstacleGridPenalty;
        public double targetAttractionStrength;
        public string cacheType;
        public string cacheDir;
        public TimeCostAttributes timeCostAttributes;
    }

    [System.Serializable]
    public class OrgVadereStateAttributesModelsAttributesOSM
    {
        public int stepCircleResolution;
        public int numberOfCircles;
        public string optimizationType;
        public bool varyStepDirection;
        public string movementType;
        public double stepLengthIntercept;
        public double stepLengthSlopeSpeed;
        public double stepLengthSD;
        public double movementThreshold;
        public double minStepLength;
        public bool minimumStepLength;
        public double maxStepDuration;
        public bool dynamicStepLength;
        public string updateType;
        public bool seeSmallWalls;
        public string targetPotentialModel;
        public string pedestrianPotentialModel;
        public string obstaclePotentialModel;
        public List<object> submodels;
    }

    [System.Serializable]
    public class OrgVadereStateAttributesModelsAttributesPotentialCompactSoftshell
    {
        public double pedPotentialIntimateSpaceWidth;
        public double pedPotentialPersonalSpaceWidth;
        public double pedPotentialHeight;
        public double obstPotentialWidth;
        public double obstPotentialHeight;
        public double intimateSpaceFactor;
        public int personalSpacePower;
        public int intimateSpacePower;
    }

    [System.Serializable]
    public class OrgVadereStateAttributesModelsPsychologyCognitionAttributesSimpleCognitionModel
    {
    }

    [System.Serializable]
    public class OrgVadereStateAttributesModelsPsychologyPerceptionAttributesSimplePerceptionModel
    {
        public Priority priority;
    }

    [System.Serializable]
    public class Priority
    {
        [JsonProperty("1")]
        public string _1;

        [JsonProperty("2")]
        public string _2;

        [JsonProperty("3")]
        public string _3;

        [JsonProperty("4")]
        public string _4;

        [JsonProperty("5")]
        public string _5;

        [JsonProperty("6")]
        public string _6;

        [JsonProperty("7")]
        public string _7;
    }

    [System.Serializable]
    public class Processor
    {
        public string type;
        public int id;
        public string attributesType;
        public Attributes attributes;
    }

    [System.Serializable]
    public class ProcessWriters
    {
        public List<File> files;
        public List<Processor> processors;
        public bool isTimestamped;
        public bool isWriteMetaData;
    }

    [System.Serializable]
    public class PsychologyLayer
    {
        public string perception;
        public string cognition;
        public AttributesModel attributesModel;
    }

    [System.Serializable]
    public class Root
    {
        public string name;
        public string description;
        public string release;
        public string commithash;
        public ProcessWriters processWriters;
        public Scenario scenario;
    }

    [System.Serializable]
    public class Scenario
    {
        public string mainModel;
        public AttributesModel attributesModel;
        public AttributesSimulation attributesSimulation;
        public AttributesPsychology attributesPsychology;
        public Topography topography;
        public List<object> stimulusInfos;
    }

    [System.Serializable]
    public class Shape
    {
        public double x;
        public double y;
        public double width;
        public double height;
        public string type;
    }

    [System.Serializable]
    public class Source
    {
        public int id;
        public Shape shape;
        public bool visible;
        public List<int> targetIds;
        public Spawner spawner;
        public List<double> groupSizeDistribution;
    }

    [System.Serializable]
    public class Spawner
    {
        public string type;
        public int constraintsElementsMax;
        public double constraintsTimeStart;
        public double constraintsTimeEnd;
        public bool eventPositionRandom;
        public bool eventPositionGridCA;
        public bool eventPositionFreeSpace;
        public int eventElementCount;
        public object eventElement;
        public Distribution distribution;
    }

    [System.Serializable]
    public class Target
    {
        public int id;
        public Shape shape;
        public bool visible;
        public Absorber absorber;
        public Waiter waiter;
        public double leavingSpeed;
        public int parallelEvents;
    }

    [System.Serializable]
    public class TimeCostAttributes
    {
        public double standardDeviation;
        public string type;
        public double obstacleDensityWeight;
        public double pedestrianSameTargetDensityWeight;
        public double pedestrianOtherTargetDensityWeight;
        public double pedestrianWeight;
        public double queueWidthLoading;
        public double pedestrianDynamicWeight;
        public string loadingType;
        public double width;
        public double height;
    }

    [System.Serializable]
    public class Topography
    {
        public Attributes attributes;
        public List<Obstacle> obstacles;
        public List<object> measurementAreas;
        public List<object> stairs;
        public List<Target> targets;
        public List<object> targetChangers;
        public List<object> absorbingAreas;
        public List<object> aerosolClouds;
        public List<object> droplets;
        public List<Source> sources;
        public List<object> dynamicElements;
        public AttributesPedestrian attributesPedestrian;
        public object teleporter;
        public AttributesCar attributesCar;
    }

    [System.Serializable]
    public class Waiter
    {
        public bool enabled;
        public object distribution;
    }


}
