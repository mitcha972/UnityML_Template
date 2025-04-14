using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SudokuAgent : Agent
{
    SudokuEnvController envController;
    private int targetIndex = 0;

    public override void Initialize()
    {
        envController = GetComponent<SudokuEnvController>();
    }

    public override void OnEpisodeBegin()
    {
        //envController.ResetPuzzle();
        targetIndex = envController.unsolvedIndex;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        int[] flatGrid = envController.GetGrid(); // 81-length array

        for (int i = 0; i < 81; i++)
        {
            for (int k = 0; k <= 9; k++) // One-hot encoding for [0 (empty), 1–9]
            {
                sensor.AddObservation(flatGrid[i] == k ? 1 : 0);
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int predictedValue = actions.DiscreteActions[0] + 1;

        bool isCorrect = envController.CheckGuess(targetIndex, predictedValue);

        if (isCorrect)
        {
            envController.ResolveEvent(StemS.CorrectNum);
            Debug.Log($"Correct! Predicted: {predictedValue}, Actual: {envController.grid.grid[targetIndex].value}");
        }
        else
        {
            envController.ResolveEvent(StemS.IncorrectNum);
            Debug.Log($"Inorrect! Predicted: {predictedValue}, Actual: {envController.grid.grid[targetIndex].value}");
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Random.Range(0, 9); // 0–8 representing 1–9
    }

}
