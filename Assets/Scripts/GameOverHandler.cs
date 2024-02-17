using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{

    [SerializeField]
    Text winnerTextBox;

    void Start()
    {
        winnerTextBox.text = GameStateManager.winnerName;
    }

}
