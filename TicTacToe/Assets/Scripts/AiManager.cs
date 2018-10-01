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
        private List<int> _emptySpaces;
        private int _randomlyChosenSpot;
        
        public void AiTurn()
        {
            _emptySpaces = BoardManager.Instance.GetEmptyBoardSpaces();
            _randomlyChosenSpot = Random.Range(0, _emptySpaces.Count);
            Debug.Log(_emptySpaces[_randomlyChosenSpot]);
            StartCoroutine(MakeAiMove());
        }

        private IEnumerator MakeAiMove()
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.MakeAMove(_emptySpaces[_randomlyChosenSpot]);
        }
    }
}
