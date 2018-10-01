///////////////////////////////////////////////////////////////
//
// BoardManager (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 10/1/2018
//
///////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

namespace Custom.Managers
{
    /// <summary>
    /// Class responsible for managing the board of the game.
    /// </summary>
    public class BoardManager : CustomSingleton<BoardManager>
    {
        /// <summary>
        /// This int array represents the board situation.
        /// 0 = empty, 1 = filled
        /// </summary>
        private int[] _board = new int[9];

        /// <summary>
        /// Method that figure out the empty spaces available on the board.
        /// </summary>
        /// <returns>A list of all the empty spaces indexes.</returns>
        public List<int> GetEmptyBoardSpaces()
        {
            var emptySpaces = new List<int>();

            for (var i = 0; i < _board.Length; i++)
            {
                if (_board[i] == 0)
                {
                    emptySpaces.Add(i);
                }
            }

            return emptySpaces;
        }

        /// <summary>
        /// Method to mark the board position as filled.
        /// </summary>
        /// <param name="position">The position to mark as filled.</param>
        public void UpdateBoardPosition(int position)
        {
            _board[position] = 1;
        }

    }
}