﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;

    MoveSelector player1move;
    MoveSelector player2move;
    MoveSelector player3move;
    MoveSelector player4move;

    MoveSelector newPlayer;

    PlayerSelector ps;

    public Text TextColorGo;
    float move_player = 5.0f;

    bool Selected = false;
    public bool is_inside = false; // Delete this later

    public int num_players;
    public int num_active;

    public int num_ready_players;

    float translationX;
    float translationY;

    Text TextColorCar;
    Text Player2text;

    
    public bool is_joy1_taken, is_arrowKeys_taken, is_wsda_taken, is_joy2_taken = false;
    public bool is_joy1_used, is_arrowKeys_used, is_wsda_used, is_joy2_used = false;
    public bool is_p1_active, is_p2_active, is_p3_active, is_p4_active = false;
  

    void Start()
    {
        num_players = 1;
        num_active = 1;
        num_ready_players = 0;

        TextColorGo = GameObject.FindWithTag("Go").GetComponent<Text>();

        player1move = player.GetComponent<MoveSelector>();
        player2move = player2.GetComponent<MoveSelector>();
        player3move = player3.GetComponent<MoveSelector>();
        player4move = player4.GetComponent<MoveSelector>();

        player.SetActive(true);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);

        is_p1_active = true;
        player1move.is_this_active = true;

        player1move.ThisPlayerControl = MoveSelector.ControlTypesHere.Joy1;
        is_joy1_taken = true;



    }


    void Update()
    {
        print("num players: " + num_players);

        print("ready players: " + num_ready_players);

        CheckWhichInput();

        CheckControlinUse();

        //If all players have selected their cars the GO text becomes green
        if (num_players == num_ready_players)
        {
            TextColorGo.color = Color.green;

            //If B or Start is pressed when GO text is green the main game is loaded
            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("SubmitJoystick")) { LoadLevel("main2"); }
        }

        else { TextColorGo.color = Color.white; }

    }

    //Function to load the main game scene
    public void LoadLevel(string name)
    {
       
        SceneManager.LoadScene(name);

    }


    void CheckWhichInput() {

        if (

       (


       ((Input.GetAxis("VerticalWSDA") != 0 || Input.GetAxis("HorizontalWSDA") != 0) && !is_wsda_taken)

                                                            ||

       ((Input.GetAxis("VerticalArrows") != 0 || Input.GetAxis("HorizontalArrows") != 0) && !is_arrowKeys_taken)

                                                            ||

       ((Input.GetAxis("VerticalJoyStickLeft2") != 0 || Input.GetAxis("HorizontalJoyStickLeft2") != 0) && !is_joy2_taken)

                                                            ||

       ((Input.GetAxis("VerticalJoyStickLeft1") != 0 || Input.GetAxis("HorizontalJoyStickLeft1") != 0) && !is_joy1_taken)


       )

                                                            &&

                                                      num_players <= 4
       )

        {
            if (Input.GetAxis("VerticalWSDA") != 0 || Input.GetAxis("HorizontalWSDA") != 0)

            { is_wsda_used = true; }


            else if (Input.GetAxis("VerticalArrows") != 0 || Input.GetAxis("HorizontalArrows") != 0)

            { is_arrowKeys_used = true; }


            else if (Input.GetAxis("VerticalJoyStickLeft2") != 0 || Input.GetAxis("HorizontalJoyStickLeft2") != 0)

            { is_joy2_used = true; }

            else if (Input.GetAxis("VerticalJoyStickLeft1") != 0 || Input.GetAxis("HorizontalJoyStickLeft1") != 0)

            { is_joy1_used = true; }

           
            Create_Player();

        }

    }

    void Create_Player()
    {
        if (is_p1_active == true && player1move.ThisPlayerControl == MoveSelector.ControlTypesHere.NotAssigned)
        {
            newPlayer = player1move;
            ps = panel1.GetComponent<PlayerSelector>();
            //ps.CPU_Controls = 1;
            setControlScheme();
        }

        else if (is_p2_active == true && player2move.ThisPlayerControl == MoveSelector.ControlTypesHere.NotAssigned)
        {
            newPlayer = player2move;
            ps = panel2.GetComponent<PlayerSelector>();
           // ps.CPU_Controls = 1;
            setControlScheme();
        }

        else if (is_p3_active == true && player3move.ThisPlayerControl == MoveSelector.ControlTypesHere.NotAssigned)
        {
            newPlayer = player3move;
            ps = panel3.GetComponent<PlayerSelector>();
            //ps.CPU_Controls = 1;
            setControlScheme();
        }

        else if (is_p4_active == true && player4move.ThisPlayerControl == MoveSelector.ControlTypesHere.NotAssigned)
        {
            newPlayer = player4move;
            ps = panel4.GetComponent<PlayerSelector>();
            //ps.CPU_Controls = 1;
            setControlScheme();
        }

        else {

        num_players++;

            switch (num_players)
        {

            case 1:
                player.SetActive(true);
                newPlayer = player1move;
                is_p1_active = true;
                ps = panel1.GetComponent<PlayerSelector>();

               // ps.CPU_Controls = 1;

                setControlScheme();

                break;

            case 2:
                player2.SetActive(true);
                newPlayer = player2move;
                is_p2_active = true;
                ps = panel2.GetComponent<PlayerSelector>();

               // ps.CPU_Controls = 1;

                setControlScheme();
                break;

            case 3:
                player3.SetActive(true);
                newPlayer = player3move;
                is_p3_active = true;
                ps = panel3.GetComponent<PlayerSelector>();

               // ps.CPU_Controls = 1;

                setControlScheme();
                break;

            case 4:
                player4.SetActive(true);
                newPlayer = player4move;
                is_p4_active = true;
                ps = panel4.GetComponent<PlayerSelector>();

               // ps.CPU_Controls = 1;

                setControlScheme();
                break;

            default:
                //num_players = 4;  //double check this
                break;

        }

            newPlayer.is_this_active = true;
            ps.CPU_Controls = 1;
            ps.switch_case = 1;

        }

       // ps.CPU_Controls = 1;
       // ps.switch_case = 1;


    }

    void setControlScheme()
    {
        

        if (!is_arrowKeys_taken && is_arrowKeys_used)

        {

            newPlayer.ThisPlayerControl = MoveSelector.ControlTypesHere.ArrowKeys;
            ps.Controls = 3;
            is_arrowKeys_taken = true;
            is_arrowKeys_used = false;

        }

        else if (!is_wsda_taken && is_wsda_used)

        {
            newPlayer.ThisPlayerControl = MoveSelector.ControlTypesHere.WSDA;
            ps.Controls = 4;
            is_wsda_taken = true;
            is_wsda_used = false;

        }

        else if (!is_joy2_taken && is_joy2_used)

        {
            newPlayer.ThisPlayerControl = MoveSelector.ControlTypesHere.Joy2;
            ps.Controls = 2;
            is_joy2_taken = true;
            is_joy2_used = false;

        }

        else if (!is_joy1_taken && is_joy1_used)

        {
            newPlayer.ThisPlayerControl = MoveSelector.ControlTypesHere.Joy1;
            ps.Controls = 1;
            is_joy1_taken = true;
            is_joy1_used = false;

        }

    }

    void CheckControlinUse() {

        if (player1move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy1
                               ||
                player2move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy1
                               ||
                player3move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy1
                               ||
                player4move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy1

           )
        { is_joy1_taken = true; }

        else { is_joy1_taken = false; }

        if (player1move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy2
                               ||
                player2move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy2
                               ||
                player3move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy2
                               ||
                player4move.ThisPlayerControl == MoveSelector.ControlTypesHere.Joy2

           )
        { is_joy2_taken = true; }

        else { is_joy2_taken = false; }

        if (player1move.ThisPlayerControl == MoveSelector.ControlTypesHere.ArrowKeys
                            ||
             player2move.ThisPlayerControl == MoveSelector.ControlTypesHere.ArrowKeys
                            ||
             player3move.ThisPlayerControl == MoveSelector.ControlTypesHere.ArrowKeys
                            ||
             player4move.ThisPlayerControl == MoveSelector.ControlTypesHere.ArrowKeys

        )
        { is_arrowKeys_taken = true; }

        else { is_arrowKeys_taken = false; }

        if (player1move.ThisPlayerControl == MoveSelector.ControlTypesHere.WSDA
                            ||
             player2move.ThisPlayerControl == MoveSelector.ControlTypesHere.WSDA
                            ||
             player3move.ThisPlayerControl == MoveSelector.ControlTypesHere.WSDA
                            ||
             player4move.ThisPlayerControl == MoveSelector.ControlTypesHere.WSDA

        )
        { is_wsda_taken = true; }

        else { is_wsda_taken = false; }



    }


}