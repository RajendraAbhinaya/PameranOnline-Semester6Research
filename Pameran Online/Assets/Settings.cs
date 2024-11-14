using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenu;
    public Transform head;
    public Transform playerCamera;
    public float spawnDistance;
    public float despawnDistance;
    public InputActionProperty showButton;
    public TMP_Text moveSpeedText;
    public TMP_Text turnSpeedText;
    public TMP_Text snapTurnText;

    [Header("Settings")]
    public AudioMixer mainMixer;
    public ActionBasedContinuousMoveProvider continuousMove;
    public ActionBasedContinuousTurnProvider continuousTurn;
    public ActionBasedSnapTurnProvider snapTurn;
    public TeleportationProvider teleportation;
    public KeyboardControls keyboard;

    [Header("Panels")]
    public GameObject[] panels;
    public GameObject exitPanel;
    public GameObject prevButton;
    public GameObject nextButton;
    public GameObject exitButton;
    public GameObject titleText;
    private int panelAmount;
    private int currPanel = 0;
    private GameObject player;
    private ActionBasedControllerManager rightHand;

    // Start is called before the first frame update
    void Start()
    {
        panelAmount = panels.Length;
        moveSpeedText.text = continuousMove.moveSpeed.ToString("#.00") + "m/s";
        turnSpeedText.text = continuousTurn.turnSpeed.ToString("#.0") + "°/s";
        snapTurnText.text = snapTurn.turnAmount.ToString("#.0") + "°";
        player = GameObject.FindWithTag("Player");
        rightHand = GameObject.Find("Right Hand").GetComponent<ActionBasedControllerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame()){
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            transform.position = head.position + playerCamera.localPosition + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }
        transform.LookAt(new Vector3(head.position.x, transform.position.y, head.position.z));
        transform.forward *= -1;

        if((transform.position - player.transform.position).magnitude > despawnDistance){
            settingsMenu.SetActive(false);
        }
    }

    //Volume Functions
    public void SetMasterVolume(float volume){
        mainMixer.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume(float volume){
        mainMixer.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume(float volume){
        mainMixer.SetFloat("sfxVolume", volume);
    }


    //Toggle Functions
    public void KeyboardToggle(bool active){
        keyboard.enabled = active;
    }

    public void SnapTurnToggle(bool active){
        snapTurn.enabled = active;
        continuousTurn.enabled = !active;
        rightHand.smoothTurnEnabled = !active;
    }

    public void TeleportationToggle(bool active){
        teleportation.enabled = active;
    }

    //Speed Functions
    public void SetMoveSpeed(float amount){
        continuousMove.moveSpeed = amount;
        moveSpeedText.text = continuousMove.moveSpeed.ToString("#.00") + "m/s";
    }

    public void SetTurnSpeed(float amount){
        continuousTurn.turnSpeed = amount;
        turnSpeedText.text = continuousTurn.turnSpeed.ToString("#.0") + "°/s";
    }

    public void SetTurnAmount(float amount){
        snapTurn.turnAmount = amount;
        snapTurnText.text = snapTurn.turnAmount.ToString("#.0") + "°";
    }

    //Used to go back to a previous panel
    public void Prev(){
        nextButton.SetActive(true);
        panels[currPanel].SetActive(false);
        if(currPanel == 0){
            currPanel = panelAmount-1;
        }
        else{
            currPanel--;
        }
        panels[currPanel].SetActive(true);
    }

    //Used to go to the next page
    public void Next(){
        prevButton.SetActive(true);
        panels[currPanel].SetActive(false);
        if(currPanel == panelAmount-1){
            currPanel = 0;
        }
        else{
            currPanel++;
        }
        panels[currPanel].SetActive(true);
    }

    public void ToggleExitPanel(bool activate){
        exitPanel.SetActive(activate);
        panels[currPanel].SetActive(!activate);
        nextButton.SetActive(!activate);
        prevButton.SetActive(!activate);
        exitButton.SetActive(!activate);
        titleText.SetActive(!activate);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
