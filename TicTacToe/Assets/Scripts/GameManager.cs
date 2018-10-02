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
    /// Note: Since there are very few UI components besides the board,
    /// I am adding their functionality in this class.
    /// </summary>
    public class GameManager : CustomSingleton<GameManager>
    {
        private const string HUMANSCOREKEY = "HumanScore";
        private const string AISCOREKEY = "AIScore";

        [Space]
        [Header("Required Components")]
        [SerializeField] private List<GridSpace> _spaces;

        [Space]
        [Header("UI Components")]
        [SerializeField, ValueRequired] private GameObject _resultPanel;
        [SerializeField, ValueRequired] private Text _resultText;
        [SerializeField, ValueRequired] private PlayerData _humanData;
        [SerializeField, ValueRequired] private PlayerData _aIData;

        private Symbol _currentSymbol;
        private int _moveCounter;
        private int _humanScore;
        private int _aIScore;
        private bool _gameEnded;


        protected override void Initialize()
        {
            base.Initialize();
            _currentSymbol = Symbol.X;
            _moveCounter = 0;
            LoadScores();
            InitializeBoardSpaces();
            _resultPanel.SetActive(false);
            SetTurnIndicator();
            _gameEnded = false;
        }

        private void InitializeBoardSpaces()
        {
            foreach (var space in _spaces)
            {
                space.MoveText = null;
                space.Enable();
            }
        }

        private void LoadScores()
        {
            _humanScore = PlayerPrefs.GetInt(HUMANSCOREKEY);
            _aIScore = PlayerPrefs.GetInt(AISCOREKEY);
        }

        private void SetTurnIndicator()
        {
            _humanData.SetIndicator(_currentSymbol);
            _aIData.SetIndicator(_currentSymbol);
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
            SaveScore();
        }

        private void SaveScore()
        {
            PlayerPrefs.SetInt(HUMANSCOREKEY, _humanScore);
            PlayerPrefs.SetInt(AISCOREKEY, _aIScore);
        }


        private void GridSpace_OnSelected(object sender, SpaceEventArgs e)
        {
            if (AiManager.Instance.AiMakingMove) return;
            MakeAMove(e.CurrentGridSpace.SpaceNumber);
            if(!_gameEnded)AiManager.Instance.AiTurn();
        }

        public void MakeAMove(int spaceNumber)
        {
            BoardManager.Instance.UpdateBoardPosition(spaceNumber);
            _spaces[spaceNumber].MoveText = _currentSymbol.ToString();
            _spaces[spaceNumber].Disable();
            _moveCounter++;

            // We only need to check for end of game after the 4th move.
            if (_moveCounter > 4)
            {
                CheckForEndOfGame();
            }

            // Switch indicator for next move.
            _currentSymbol = _currentSymbol == Symbol.X ? Symbol.O : Symbol.X;
            SetTurnIndicator();
        }

        /// <summary>
        /// Method referenced by the restart button in the editor,
        /// used to reset the game to the beginning.
        /// </summary>
        public void Restart()
        {
            SaveScore();
            Initialize();
            if (AiManager.HasInstance) AiManager.Instance.Restart();
            if (BoardManager.HasInstance) BoardManager.Instance.Restart();
        }

        /// <summary>
        /// Method referenced by the reset scores button in the editor,
        /// used to reset scores to 0.
        /// </summary>
        public void ResetScore()
        {
            _humanScore = 0;
            _aIScore = 0;
            SaveScore();
            UpdateScores();
        }

        /// <summary>
        /// Checks rows, columns and diagonals to see if they are the same.
        /// Since it's only 8 possible outcomes we are brute forcing it for now.
        /// TODO: Figure out a better way to check for a win.
        /// </summary>
        private void CheckForEndOfGame()
        {
            if (_spaces[0].MoveText == _currentSymbol.ToString() && _spaces[1].MoveText == _currentSymbol.ToString() && _spaces[2].MoveText == _currentSymbol.ToString())
            {
                EndGame();
            }

            if (_spaces[3].MoveText == _currentSymbol.ToString() && _spaces[4].MoveText == _currentSymbol.ToString() && _spaces[5].MoveText == _currentSymbol.ToString())
            {
                EndGame();
            }

            if (_spaces[6].MoveText == _currentSymbol.ToString() && _spaces[7].MoveText == _currentSymbol.ToString() && _spaces[8].MoveText == _currentSymbol.ToString())
            {
                EndGame();
            }

            if (_spaces[0].MoveText == _currentSymbol.ToString() && _spaces[3].MoveText == _currentSymbol.ToString() && _spaces[6].MoveText == _currentSymbol.ToString())
            {              
                EndGame();
            }              
                           
            if (_spaces[1].MoveText == _currentSymbol.ToString() && _spaces[4].MoveText == _currentSymbol.ToString() && _spaces[7].MoveText == _currentSymbol.ToString())
            {              
                EndGame();
            }              
                           
            if (_spaces[2].MoveText == _currentSymbol.ToString() && _spaces[5].MoveText == _currentSymbol.ToString() && _spaces[8].MoveText == _currentSymbol.ToString())
            {              
                EndGame();
            }              
                           
            if (_spaces[0].MoveText == _currentSymbol.ToString() && _spaces[4].MoveText == _currentSymbol.ToString() && _spaces[8].MoveText == _currentSymbol.ToString())
            {              
                EndGame();
            }              
                           
            if (_spaces[2].MoveText == _currentSymbol.ToString() && _spaces[4].MoveText == _currentSymbol.ToString() && _spaces[6].MoveText == _currentSymbol.ToString())
            {
                EndGame();
            }

            if (_moveCounter >= 9 && !_gameEnded)
            {
                EndGame(true);
            }
        }

        /// <summary>
        /// Anything neede to be done at the end of the game should be implemented here.
        /// </summary>
        private void EndGame(bool isDraw=false)
        {
            if (!_gameEnded) _gameEnded = true;
            else return;

            if (isDraw)
            {
                _resultText.text = "DRAW!";
            }
            else
            {
                if (_currentSymbol.ToString().Equals("X"))
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
            _humanData.SetScore(_humanScore);
            _aIData.SetScore(_aIScore);
        }


    }

    public enum Symbol
    {
        X,
        O
    }
}
