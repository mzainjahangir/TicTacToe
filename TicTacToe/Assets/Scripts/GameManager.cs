///////////////////////////////////////////////////////////////
//
// GameManager (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/29/2018
//
///////////////////////////////////////////////////////////////

using System.Collections.Generic;
using Custom.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Managers
{
    /// <summary>
    /// Class responsible for managing everything related to
    /// the core functionality of the game.
    /// </summary>
    public class GameManager : CustomSingleton<GameManager>
    {
        [Space]
        [Header("Required Components")]
        [SerializeField] private List<GridSpace> _spaces;

        [Space]
        [Header("UI Components")]
        [SerializeField, ValueRequired] private GameObject _resultPanel;
        [SerializeField, ValueRequired] private Text _resultText;
        [SerializeField, ValueRequired] private Text _humanScoreText;
        [SerializeField, ValueRequired] private Text _aIScoreText;

        private const string HUMANSCOREKEY = "HumanScore";
        private const string AISCOREKEY = "AIScore";

        private string _playerIndicator;
        private int _moveCounter;
        private int _humanScore;
        private int _aIScore;

        protected override void Initialize()
        {
            base.Initialize();
            _humanScore = PlayerPrefs.GetInt(HUMANSCOREKEY);
            _aIScore = PlayerPrefs.GetInt(AISCOREKEY);
            _playerIndicator = "X";
            _resultPanel.SetActive(false);
        }

        protected virtual void Start()
        {
            foreach (var gridSpace in _spaces)
            {
                gridSpace.Selected += GridSpace_OnSelected;
            }
            UpdateScores();
        }

        protected virtual void OnDisable()
        {
            PlayerPrefs.SetInt(HUMANSCOREKEY, _humanScore);
            PlayerPrefs.SetInt(AISCOREKEY, _aIScore);
        }


        private void GridSpace_OnSelected(object sender, SpaceEventArgs e)
        {
            if (AiManager.Instance.AiMakingMove) return;
            MakeAMove(e.CurrentGridSpace.SpaceNumber);
            if(!_resultPanel.activeSelf)AiManager.Instance.AiTurn();
        }

        public void MakeAMove(int spaceNumber)
        {
            BoardManager.Instance.UpdateBoardPosition(spaceNumber);
            _spaces[spaceNumber].MoveText = _playerIndicator;
            _spaces[spaceNumber].Disable();
            _moveCounter++;

            // We only need to check for end of game after the 4th move.
            if (_moveCounter > 4)
            {
                CheckForEndOfGame();
            }

            // Switch indicator for next move.
            _playerIndicator = _playerIndicator.Equals("X") ? "O" : "X";
        }

        /// <summary>
        /// Checks rows, columns and diagonals to see if they are the same.
        /// Since it's only 8 possible outcomes we are brute forcing it for now.
        /// TODO: Figure out a better way to check for a win.
        /// </summary>
        private void CheckForEndOfGame()
        {
            if (_spaces[0].MoveText == _playerIndicator && _spaces[1].MoveText == _playerIndicator && _spaces[2].MoveText == _playerIndicator)
            {
                EndGame();
            }

            if (_spaces[3].MoveText == _playerIndicator && _spaces[4].MoveText == _playerIndicator && _spaces[5].MoveText == _playerIndicator)
            {
                EndGame();
            }

            if (_spaces[6].MoveText == _playerIndicator && _spaces[7].MoveText == _playerIndicator && _spaces[8].MoveText == _playerIndicator)
            {
                EndGame();
            }

            if (_spaces[0].MoveText == _playerIndicator && _spaces[3].MoveText == _playerIndicator && _spaces[6].MoveText == _playerIndicator)
            {              
                EndGame();
            }              
                           
            if (_spaces[1].MoveText == _playerIndicator && _spaces[4].MoveText == _playerIndicator && _spaces[7].MoveText == _playerIndicator)
            {              
                EndGame();
            }              
                           
            if (_spaces[2].MoveText == _playerIndicator && _spaces[5].MoveText == _playerIndicator && _spaces[8].MoveText == _playerIndicator)
            {              
                EndGame();
            }              
                           
            if (_spaces[0].MoveText == _playerIndicator && _spaces[4].MoveText == _playerIndicator && _spaces[8].MoveText == _playerIndicator)
            {              
                EndGame();
            }              
                           
            if (_spaces[2].MoveText == _playerIndicator && _spaces[4].MoveText == _playerIndicator && _spaces[6].MoveText == _playerIndicator)
            {
                EndGame();
            }

            if (_moveCounter > 9)
            {
                EndGame(true);
            }
        }

        /// <summary>
        /// Anything neede to be done at the end of the game should be implemented here.
        /// </summary>
        private void EndGame(bool isDraw=false)
        {
            if (isDraw)
            {
                _resultText.text = "DRAW!";
            }
            else
            {
                if (_playerIndicator.Equals("X"))
                {
                    _humanScore++;
                    _resultText.text = "HUMAN WON!";
                }
                else
                {
                    _aIScore++;
                    _resultText.text = "AI WON!";
                }
                
                UpdateScores();
            }

            _resultPanel.SetActive(true);

            DisableAllSpaces();
        }

        private void DisableAllSpaces()
        {
            foreach (var gridSpace in _spaces)
            {
                gridSpace.Disable();
            }
        }

        private void UpdateScores()
        {
            _humanScoreText.text = _humanScore.ToString();
            _aIScoreText.text = _aIScore.ToString();
        }
    }
}
