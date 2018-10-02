///////////////////////////////////////////////////////////////
//
// BoardManager (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 10/1/2018
//
///////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Custom.Managers
{
    /// <summary>
    /// Class responsible for managing the board of the game.
    /// </summary>
    public class BoardManager : CustomSingleton<BoardManager>
    {
        #region Member Variables

        /// <summary>
        /// This int array represents the board current situation.
        /// 0 = empty, 1 = filled
        /// </summary>
        private readonly int[] _board = new int[9];

        #endregion

        #region Private Methods

        private void ResetBoard()
        {
            for (var i = 0; i < _board.Length; i++)
            {
                _board[i] = 0;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to be called every time the player hits Restart.
        /// </summary>
        public void Restart()
        {
            ResetBoard();
        }

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
        public void FillBoardPosition(int position)
        {
            if (_board.Length > position) _board[position] = 1;
        }

        #endregion
    }
}