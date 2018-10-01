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
        [SerializeField] private List<GridSpace> _spaces;

        [SerializeField, ValueRequired] private GameObject _resultPanel;
        [SerializeField, ValueRequired] private Text _resultText;
        
        private string _playerIndicator;
        private int _moveCounter;

        protected virtual void Start()
        {
            _playerIndicator = "X";
            _resultPanel.SetActive(false);

            foreach (var gridSpace in _spaces)
            {
                gridSpace.Selected += GridSpace_OnSelected;
            }
        }

        private void GridSpace_OnSelected(object sender, SpaceEventArgs e)
        {
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
                _resultText.text = _playerIndicator + " WON!";
            }

            _resultPanel.SetActive(true);

            foreach (var gridSpace in _spaces)
            {
                gridSpace.Disable();
            }
        }
    }
}
