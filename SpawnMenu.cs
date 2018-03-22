using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnMenu : MonoBehaviour {

	public static bool SimulationIsPaused = false;
	public GameObject playerMenuUI;
	public GameObject spawnMenuUI;
	public Spawn spawner;
	
	public void Resume()
	{
		spawnMenuUI.SetActive(false); 
		Time.timeScale = 1f;
		SimulationIsPaused = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void Pause()
	{
		spawnMenuUI.SetActive(true);
		Time.timeScale = 0f;
		SimulationIsPaused = true;
		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	
	public void Spawn()
	{
		spawner.SpawnPart();
		playerMenuUI.SetActive(true);
		spawnMenuUI.SetActive(false);
	}
}
