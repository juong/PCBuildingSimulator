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
	
	public void PlayerMenu()
	{
		spawnMenuUI.SetActive(false); 
		playerMenuUI.SetActive(true);
	}
	
	
	public void SpawnCPU() {
		spawner.SpawnCPU();
	}
	
	public void SpawnCPUF() {
		spawner.SpawnCPUF();
	}
	
	public void SpawnGPU() {
		spawner.SpawnGPU();
	}
	
	public void SpawnStorage() {
		spawner.SpawnStorage();
	}
	
	public void SpawnRAM() {
		spawner.SpawnRAM();
	}
	
	public void SpawnPSU() {
		spawner.SpawnPSU();
	}

    public void SpawnMB()
    {
        spawner.SpawnMB();
    }

    public void SpawnCD()
    {
        spawner.SpawnCD();
    }

    public void SpawnKBD()
    {
        spawner.SpawnKBD();
    }

    public void SpawnMOU()
    {
        spawner.SpawnMOU();
    }
}
