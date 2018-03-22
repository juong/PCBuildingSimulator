using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool SimulationIsPaused = false;
	
	public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if ( SimulationIsPaused )
			{
				Resume();
			} else
			{
				Pause();
			}
		}
	}
	
	public void Resume()
	{
		pauseMenuUI.SetActive(false); 
		Time.timeScale = 1f;
		SimulationIsPaused = false;
	}
	
	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		SimulationIsPaused = true;
		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	
	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
	}
	
	public void QuitSimulation()
	{
		Application.Quit();
	}
}
