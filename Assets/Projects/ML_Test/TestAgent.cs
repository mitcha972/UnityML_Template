using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class TestAgent : Agent
{
    public GameObject area; 
    TestEnvController envController;

    void Start()
    {
        envController = area.GetComponent<TestEnvController>();
    }

    void Update()
    {
        Training();
    }

    void Training()
    {
        //Where i add things to effect the training

        //aim at goal
        // Check if the ray hits the "Goal" object
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
        {
            if (hit.collider.CompareTag("goal"))
            {
                //Debug.Log("SeaGoal!!");
                SetReward(+ 0.1f);
            }
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent position
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.z);
        
        // Agent rotation around the y-axis, normalized to range [-1, 1]
        float normalizedYRotation = transform.eulerAngles.y / 360.0f;
        sensor.AddObservation(normalizedYRotation);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveZ = actions.ContinuousActions[0];
        float moveX = actions.ContinuousActions[1];
        float moveSpeed = 4f;

        // Move forward/backward and sideways
        //transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        // Get the agent's local forward direction
        Vector3 forwardDirection = transform.forward;

        // Move forward/backward and sideways based on the local direction
        transform.localPosition += (forwardDirection * moveZ + transform.right * moveX) * Time.deltaTime * moveSpeed;

        // Get the discrete action for rotation
        int rotateAction = actions.DiscreteActions[0];
        float rotateSpeed = 100f; // Adjust this value as needed

        // Determine the rotation direction based on the discrete action
        if (rotateAction == 1) // Rotate left
        {
            SetReward(-0.01f);
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        else if (rotateAction == 2) // Rotate right
        {
            SetReward(-0.01f);
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
                
    }

    //User Control 
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        var discreteActionsOut = actionsOut.DiscreteActions;
        
        // Example control scheme:
        // Movement: Arrow keys or WASD
        // Rotation: Q and E for left and right rotation

        // Movement
        continuousActionsOut[0] = Input.GetAxis("Vertical"); // Forward/backward
        continuousActionsOut[1] = Input.GetAxis("Horizontal"); // Left/right

        // Rotation - assuming you have a discrete action for rotation
        // This is a simple scheme where pressing Q rotates left, and E rotates right.
        // Adjust the indices and values according to your action setup.
        if (Input.GetKey(KeyCode.Q))
        {
            discreteActionsOut[0] = 1; // Example value for rotating left
        }
        else if (Input.GetKey(KeyCode.E))
        {
            discreteActionsOut[0] = 2; // Example value for rotating right
        }
        else
        {
            discreteActionsOut[0] = 0; // No rotation or default rotation state
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            envController.ResolveEvent(Stem.HitGoal); // Pass the End Resolve to the controller 
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            envController.ResolveEvent(Stem.HitWall); // Pass the End Resolve to the controller
        }
    }
}
