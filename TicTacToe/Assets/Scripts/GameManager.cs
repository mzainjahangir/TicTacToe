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

namespace Custom.Managers
{
    /// <summary>
    /// Class responsible for managing everything related to
    /// the core functionality of the game.
    /// </summary>
    public class GameManager : CustomSingleton<GameManager>
    {
        [SerializeField] private List<GridSpace> _spaces;

        private string _playerIndicator;
        private int _moveCounter;

        protected virtual void Start()
        {
            _playerIndicator = "X";
            foreach (var gridSpace in _spaces)
            {
                gridSpace.Selected += GridSpace_OnSelected;
            }
        }

        private void GridSpace_OnSelected(object sender, SpaceEventArgs e)
        {
            e.CurrentGridSpace.MoveText = _playerIndicator;
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
        }

        private void EndGame()
        {
            Debug.Log( _playerIndicator + " Won");
            foreach (var gridSpace in _spaces)
            {
                gridSpace.Disable();
            }
        }
    }
}
