using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour {

	public static bool SimulationIsPaused = false;
	
	public GameObject playerMenuUI;
	public GameObject spawnMenuUI;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M))
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
		playerMenuUI.SetActive(false); 
		Time.timeScale = 1f;
		SimulationIsPaused = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void Pause()
	{
		playerMenuUI.SetActive(true);
		Time.timeScale = 0f;
		SimulationIsPaused = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	
	public void Spawn()
	{
		spawnMenuUI.SetActive(true); 
		playerMenuUI.SetActive(false);
	}
}
