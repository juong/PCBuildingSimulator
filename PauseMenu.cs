using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public static bool SimulationIsPaused = false;
    public static string partPath;
    public static string casePath;
    public GameObject pauseMenuUI;
	public GameObject playerMenuUI;

    GameObject player;
    float camX, camY; //used for storing default values

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !playerMenuUI.activeSelf)
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
        player.GetComponent<PlayerControls>().camSpeedX = camX;
        player.GetComponent<PlayerControls>().camSpeedY = camY;
        SimulationIsPaused = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
        camX = 2;
        camY = 5;
        player.GetComponent<PlayerControls>().camSpeedX = 0;
        player.GetComponent<PlayerControls>().camSpeedY = 0;
        SimulationIsPaused = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

    void Awake()
    {
        casePath = System.IO.Path.Combine(Application.dataPath, "Resources/case.txt");
        partPath = System.IO.Path.Combine(Application.dataPath, "Resources/parts.txt");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Save()
    {
        SaveLoadData.Save(partPath, casePath, SaveLoadData.partCollection);
    }

    public void Load()
    {
        this.transform.GetChild(0).GetChild(2).GetChild(1).gameObject.GetComponent<SaveLoadData>().Load(partPath, casePath);
    }

    public void LoadMenu()
	{
        Resume();
		SceneManager.LoadScene("Menu");
	}
	
	public void QuitSimulation()
	{
		Application.Quit();
	}

    public void SwitchHand()
    {
        Transform t = player.transform.GetChild(0).GetChild(0);
        GameObject text = this.transform.GetChild(0).GetChild(5).GetChild(0).gameObject;
        bool switched = false;
        for(int i = 0; i < t.childCount; i++)
        {
            if (!switched)
            {
                if (t.GetChild(i).gameObject.activeSelf)
                {
                    t.GetChild(i).gameObject.SetActive(false);
                    if (i < t.childCount - 1)
                    {
                        t.GetChild(i + 1).gameObject.SetActive(true);
                        text.GetComponent<Text>().text = "Current tool: " + t.GetChild(i + 1).gameObject.name;
                    }
                    else
                    {
                        t.GetChild(0).gameObject.SetActive(true);
                        text.GetComponent<Text>().text = "Current tool: " + t.GetChild(0).gameObject.name;
                        player.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Grab>().item = null;
                    }
                    switched = true;
                }
            }
        }
    }
}
