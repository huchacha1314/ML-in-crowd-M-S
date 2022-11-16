import json


def read_scenario(path="scenarios/corner.scenario"):
    with open(path, 'r') as f:
        scenario = json.load(f)
        return scenario


def add_pedestrian(scenario=None, scenario_path=None, out_scen_name=None, output_path=None, id=None,
                   find_min_free_id=True, targetIds=[], radius=0.2, densityDependentSpeed=False,
                   speedDistributionMean=1.34, speedDistributionStandardDeviation=0.26, minimumSpeed=0.5,
                   maximumSpeed=2.2, acceleration=2.0, footstepHistorySize=4, searchRadius=1.0,
                   walkingDirectionCalculation="BY_TARGET_CENTER", walkingDirectionSameIfAngleLessOrEqual=45.0,
                   nextTargetListIndex=0, position=(0, 0), velocity=(0, 0), freeFlowSpeed=1.8522156059160915,
                   followers=[], idAsTarget=-1, infectionStatus="SUSCEPTIBLE", lastInfectionStatusUpdateTime=-1.0,
                   pathogenAbsorbedLoad=0.0, groupIds=[], groupSizes=[], agentsInGroup=[], traj_footsteps=[]):
    
    scenario = read_scenario(scenario_path)

    if not targetIds and len(scenario['scenario']['topography']['targets']) == 1:
        targetIds = [scenario['scenario']['topography']['targets'][0]['id']]

    ped = {
        "attributes": {
            "id": id,
            "radius": radius,
            "densityDependentSpeed": densityDependentSpeed,
            "speedDistributionMean": speedDistributionMean,
            "speedDistributionStandardDeviation": speedDistributionStandardDeviation,
            "minimumSpeed": minimumSpeed,
            "maximumSpeed": maximumSpeed,
            "acceleration": acceleration,
            "footstepHistorySize": footstepHistorySize,
            "searchRadius": searchRadius,
            "walkingDirectionCalculation": walkingDirectionCalculation,
            "walkingDirectionSameIfAngleLessOrEqual": walkingDirectionSameIfAngleLessOrEqual
        },
        "source": None,
        "targetIds": targetIds,
        "nextTargetListIndex": nextTargetListIndex,
        "isCurrentTargetAnAgent": False,
        "position": {
            "x": float(position[0]),
            "y": float(position[1])
        },
        "velocity": {
            "x": velocity[0],
            "y": velocity[1]
        },
        "freeFlowSpeed": freeFlowSpeed,
        "followers": followers,
        "idAsTarget": idAsTarget,
        "isChild": False,
        "isLikelyInjured": False,
        "psychologyStatus": {
            "mostImportantStimulus": None,
            "threatMemory": {
                "allThreats": [],
                "latestThreatUnhandled": False
            },
            "selfCategory": "TARGET_ORIENTED",
            "groupMembership": "OUT_GROUP",
            "knowledgeBase": {
                "knowledge": [],
                "informationState": "NO_INFORMATION"
            },
            "perceivedStimuli": [],
            "nextPerceivedStimuli": []
        },
        "healthStatus": {
            "infectionStatus": infectionStatus,
            "lastInfectionStatusUpdateTime": lastInfectionStatusUpdateTime,
            "pathogenAbsorbedLoad": pathogenAbsorbedLoad,
            "startBreatheOutPosition": None,
            "respiratoryTimeOffset": -1.0,
            "breathingIn": False,
            "pathogenEmissionCapacity": -1.0,
            "pathogenAbsorptionRate": -1.0,
            "minInfectiousDose": -1.0,
            "exposedPeriod": -1.0,
            "infectiousPeriod": -1.0,
            "recoveredPeriod": -1.0
        },
        "groupIds": groupIds,
        "groupSizes": groupSizes,
        "agentsInGroup": agentsInGroup,
        "trajectory": {"footSteps": traj_footsteps},
        "modelPedestrianMap": None,
        "type": "PEDESTRIAN"
    }

    scenario['scenario']['topography']['dynamicElements'].append(ped)

    if output_path is None:  
        output_path = scenario_path
    elif not output_path.endswith(".scenario"):  
        output_path += ".scenario"

    if out_scen_name is not None:
        scenario['name'] = out_scen_name

    with open(output_path, 'w') as f:
        json.dump(scenario, f, indent='  ')


if __name__ == '__main__':
    add_pedestrian(
        scenario_path="../task1/scenarios/corner.scenario",
        out_scen_name="task3",
        output_path="scenarios/corner.scenario",
        position=(8, 3),
        targetIds=[5]
    )
