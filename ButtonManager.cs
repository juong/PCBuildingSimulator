using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void BeginSimulation(string newSimulation)
    {
        SceneManager.LoadScene(newSimulation);
    }
    
    public void LeaveSimulation()
    {
        Application.Quit();
    }
}
