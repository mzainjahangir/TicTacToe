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

        private bool _isX;

        protected virtual void Start()
        {
            foreach (var gridSpace in _spaces)
            {
                gridSpace.Selected += GridSpace_OnSelected;
            }
        }

        private void GridSpace_OnSelected(object sender, SpaceEventArgs e)
        {
            if (_isX)
            {
                e.CurrentGridSpace.MoveText.text = "X";
                _isX = false;
            }
            else
            {
                e.CurrentGridSpace.MoveText.text = "O";
                _isX = true;
            }

            Debug.Log("Space #" + e.CurrentGridSpace.SpaceNumber + " Selected");
        }
    }
}
