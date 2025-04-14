using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StemS
{
    IncorrectNum = 0,
    CorrectNum = 1,
}

public class SudokuEnvController : MonoBehaviour
{    
    //Sudoku NNET
    public SudokuAgent agent;

    //Sudoku Boadrd Game Object
    public SudokuGrid grid;

    //Render 
    public Renderer renderer;
    public Material winMaterial;
    public Material loseMaterial;
    Material originalMaterial; 

    private int resetTimer;

    public int unsolvedIndex;

    void Start()
    {      
        ResetScene();
    }

    public void ResolveEvent(StemS triggerStem)
    {
        switch (triggerStem)
        {
            case StemS.CorrectNum:
                agent.SetReward(+1f);
                Debug.Log("Correct");
                //StartCoroutine(RenderMaterial(winMaterial, renderer,0.5f));
                break;

            case StemS.IncorrectNum:
                agent.SetReward(-1f);
                Debug.Log("Incorrect");
                //StartCoroutine(RenderMaterial(loseMaterial, renderer, 0.5f));
                break;
        }

        agent.EndEpisode();
        ResetScene(); // Move this outside of the switch statement if you want it to execute regardless of the triggerStem
    }


    IEnumerator RenderMaterial(Material mat, Renderer thisRenderer, float time)
    {    
        //Debug.Log("Render on");
        //Material originalMaterial  =  thisRenderer.material;
        thisRenderer.material = mat;

        yield return new WaitForSeconds(time); // wait for 2 sec
       
        thisRenderer.material = originalMaterial;

    }

    public void ResetScene()
    {
        resetTimer = 0;

        //Reset Sudoku Visual
        //REset Sudoku Grid
        grid.GenerateGrid();
        unsolvedIndex = grid.unsolvedIndex;
    }

    public int[] GetGrid()
    {  
        int[] getgrid = new int[81];
        
        for (int i = 0; i < 81; i++)
        {
            getgrid[i] = grid.grid[i].value;
        }

        return getgrid;
    }

    public bool CheckGuess(int targetIndex, int predictedValue)
    {
        return grid.testgrid[targetIndex] == predictedValue;
    }

    public int GetTestValueAt(int index)
    {
        if (index < 0 || index >= 81) return -1;
        return grid.grid[index].value;
    }

}
