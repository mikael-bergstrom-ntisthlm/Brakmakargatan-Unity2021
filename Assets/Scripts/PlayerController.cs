using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    string fighterName;

    [SerializeField]
    PlayerController opponent;
    
    public PlayerController Opponent
    {
        private set
        {
            opponent = value;
        }
        get
        {
            return opponent;
        }
    }

    private void Start()
    {
        Opponent = FindOpponent();
    }

    public string FighterName
    {
        private set
        {
            fighterName = value;
        }
        get
        {
            return fighterName;
        }
    }

    public string controlPrefix = "P1";

    public string VerticalAxis
    {
        private set { }
        get
        {
            return controlPrefix + "Vertical";
        }
    }

    public string HorizontalAxis
    {
        private set { }
        get
        {
            return controlPrefix + "Horizontal";
        }
    }

    public string PunchAxis
    {
        private set { }
        get
        {
            return controlPrefix + "Fire1";
        }
    }

    public string KickAxis
    {
        private set { }
        get
        {
            return controlPrefix + "Fire2";
        }
    }

    public PlayerController FindOpponent()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject candidate in candidates)
        {
            if (candidate != this.gameObject)
            {
                return candidate.GetComponent<PlayerController>();
            }
        }

        return this;
    }
}
