///////////////////////////////////////////////////////////////
//
// AiManager (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 10/1/2018
//
///////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Managers
{
    /// <summary>
    /// Class responsible for managing the operations of AI.
    /// </summary>
    public class AiManager : CustomSingleton<AiManager>
    {
        #region References

        [Range(0,5)] [Tooltip("Used to give the illusion that the AI is taking time to make it's move.")]
        [SerializeField] private float _aIMoveDelay;
        [Tooltip("Reference to the game object that will show the AI is computing it's next move.")]
        [SerializeField, ValueRequired] private GameObject _computingMoveIndicator;

        #endregion

        #region Member Variables

        /// <summary>
        /// Will return true if the AI is in the middle of making a move.
        /// </summary>
        public bool AiMakingMove { get; private set; }

        private List<int> _emptySpaces;
        private int _randomlyChosenSpot;

        #endregion

        #region Private Methods

        private IEnumerator MakeAiMove()
        {
            StartAiMove();
            yield return new WaitForSeconds(_aIMoveDelay);
            GameManager.Instance.MakeAMove(_emptySpaces[_randomlyChosenSpot]);
            EndAiMove();
        }

        private void StartAiMove()
        {
            AiMakingMove = true;
            _computingMoveIndicator.SetActive(true);
        }
        private void EndAiMove()
        {
            _computingMoveIndicator.SetActive(false);
            AiMakingMove = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to make the Ai take turn.
        /// </summary>
        public void AiTurn()
        {
            _emptySpaces = BoardManager.Instance.GetEmptyBoardSpaces();
            _randomlyChosenSpot = Random.Range(0, _emptySpaces.Count);
            StartCoroutine(MakeAiMove());
        }

        /// <summary>
        /// Method to be called every time the player hits Restart.
        /// </summary>
        public void Restart()
        {
            StopAllCoroutines();
            EndAiMove();
        }

        #endregion
    }
}
