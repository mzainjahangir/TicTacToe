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
    /// Class responsible for managing the AI.
    /// </summary>
    public class AiManager : CustomSingleton<AiManager>
    {
        [Range(0,5)]
        [SerializeField] private float _aIMoveDelay;

        [SerializeField, ValueRequired] private GameObject _computingMoveIndicator;

        private List<int> _emptySpaces;
        private int _randomlyChosenSpot;

        public bool AiMakingMove { get; private set; }

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

        
    }
}
