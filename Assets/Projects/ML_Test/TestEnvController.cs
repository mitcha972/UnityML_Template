using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stem
    {
        HitGoal = 0,
        HitWall = 1,
    }

public class TestEnvController : MonoBehaviour
{
    
    //agent
    public TestAgent agent;
    Transform agentPos;

    public GameObject goal;
    Transform goalPos;

    //Render 
    public Renderer renderer;

    public Material winMaterial;
    public Material loseMaterial;
    Material originalMaterial; 

    private int resetTimer;

    void Start()
    {
        // Used to control agent & ball starting positions
        agentPos = agent.GetComponent<Transform>();
        goalPos = goal.GetComponent<Transform>();
        originalMaterial  =  renderer.material;
      
        ResetScene();
    }

     /// <summary>
    /// Called every step. Control max env steps.
    /// </summary>
    void FixedUpdate()
    {
        resetTimer += 1;

        if (resetTimer >= 5000 && 5000 > 0)
        {
            agent.EpisodeInterrupted();
        
            ResetScene();
        }
    }

    public void ResolveEvent(Stem triggerStem)
    {
        switch (triggerStem)
        {
            case Stem.HitGoal:
                agent.SetReward(+10f);
                agent.EndEpisode();
                StartCoroutine(RenderMaterial(winMaterial, renderer,0.5f));
                Debug.Log("Goal");
                break;

            case Stem.HitWall:
                agent.SetReward(-1f);
                agent.EndEpisode();
                Debug.Log("Fail");
                StartCoroutine(RenderMaterial(loseMaterial, renderer, 0.5f));
                break;
        }
        ResetScene(); // Move this outside of the switch statement if you want it to execute regardless of the triggerStem
    }


    IEnumerator RenderMaterial(Material mat, Renderer thisRenderer, float time)
    {    
        //Debug.Log("Render on");
        //Material originalMaterial  =  thisRenderer.material;
        thisRenderer.material = mat;

        yield return new WaitForSeconds(time); // wait for 2 sec
       
        thisRenderer.material = originalMaterial;

        // Debug.Log("Render Off");

    }

    public void ResetScene()
    {
        resetTimer = 0;

        agent.transform.localPosition = new Vector3(0f, 0f, -4f);
        agent.transform.localRotation = Quaternion.identity; // Reset rotation to zero
           
        // reset goal to starting conditions
        Resetgoal();
    }

    void Resetgoal()
    {
        var randomPosX = Random.Range(-4f, 4f);
        var randomPosZ = Random.Range(-5f, 5f);
        goal.transform.localPosition = new Vector3(randomPosX, 0f, 0f);

        //goal.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

}
