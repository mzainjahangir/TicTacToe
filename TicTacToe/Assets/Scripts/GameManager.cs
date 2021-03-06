///////////////////////////////////////////////////////////////
//
// GameManager (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/29/2018
//
///////////////////////////////////////////////////////////////

using System.Collections.Generic;
using Custom.UI;
using JetBrains.Annotations;
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
        #region Constants

        private const string HUMANSCOREKEY = "HumanScore";
        private const string AISCOREKEY = "AIScore";

        #endregion

        #region References

        [Space]
        [Header("Required Components")]

        [Tooltip("Reference to all the 9 spaces on the board.")]
        [SerializeField] private List<GridSpace> _spaces;

        [Space]
        [Header("UI Components")]
        [SerializeField, ValueRequired] private PlayerData _humanData;
        [SerializeField, ValueRequired] private PlayerData _aIData;
        [SerializeField, ValueRequired] private GameObject _selectionPanel;
        [SerializeField, ValueRequired] private GameObject _resultPanel;
        [SerializeField, ValueRequired] private Text _resultText;

        #endregion

        #region Member Variables

        private int _moveCounter;
        private int _humanScore;
        private int _aIScore;
        private bool _gameEnded;
        private Symbol _humanSymbol;
        private Symbol _aISymbol;
        private Symbol _currentSymbol;

        #endregion

        #region Initializaion & Destruction

        protected override void Initialize()
        {
            base.Initialize();
            // In the beginning the current symbol will always be human symbol
            // because the player always makes the first move in this game.
            _currentSymbol = _humanSymbol;
            _moveCounter = 0;
            LoadScores();
            InitializeBoardSpaces();
            _resultPanel.SetActive(false);
            SetTurnIndicator();
            _gameEnded = false;
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

        #endregion

        #region Private Methods

        private void GridSpace_OnSelected(object sender, SpaceEventArgs spaceEventArgs)
        {
            if (AiManager.Instance.AiMakingMove) return;

            MakeAMove(spaceEventArgs.CurrentGridSpace.SpaceNumber);

            if (!_gameEnded) AiManager.Instance.AiTurn();
        }

        /// <summary>
        /// Enables all board spaces and sets their text to null.
        /// </summary>
        private void InitializeBoardSpaces()
        {
            foreach (var space in _spaces)
            {
                space.MoveText = null;
                space.Enable();
            }
        }

        /// <summary>
        /// Sets the turn indicator on both sides.
        /// </summary>
        private void SetTurnIndicator()
        {
            _humanData.SetTurnIndicator(_currentSymbol);
            _aIData.SetTurnIndicator(_currentSymbol);
        }

        private void SetSymbolIndicators()
        {
            _humanData.SetSymbol(_humanSymbol);
            _aIData.SetSymbol(_aISymbol);
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
        private void EndGame(bool isDraw = false)
        {
            if (!_gameEnded) _gameEnded = true;
            else return;

            if (isDraw)
            {
                _resultText.text = "DRAW!";
            }
            else
            {
                if (_currentSymbol == _humanSymbol)
                {
                    _humanScore++;
                    _resultText.text = "YOU WON!";
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

        #region Player Preferences

        // TODO: Create a seperate class to handle the player preferences.

        /// <summary>
        /// Saves the current score to the player prefs
        /// </summary>
        private void SaveScore()
        {
            PlayerPrefs.SetInt(HUMANSCOREKEY, _humanScore);
            PlayerPrefs.SetInt(AISCOREKEY, _aIScore);
        }

        /// <summary>
        /// Loads the scores from player preferences.
        /// </summary>
        private void LoadScores()
        {
            _humanScore = PlayerPrefs.GetInt(HUMANSCOREKEY);
            _aIScore = PlayerPrefs.GetInt(AISCOREKEY);
        }

        #endregion
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Method that play a move on the given space.
        /// </summary>
        /// <param name="spaceNumber">The space number you want to play the move at.</param>
        public void MakeAMove(int spaceNumber)
        {
            BoardManager.Instance.FillBoardPosition(spaceNumber);
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

        [UsedImplicitly]
        public void SelectSymbolX()
        {
            _humanSymbol = Symbol.X;
            _aISymbol = Symbol.O;
            _currentSymbol = _humanSymbol;
            _selectionPanel.SetActive(false);
            SetSymbolIndicators();
        }

        [UsedImplicitly]
        public void SelectSymbolO()
        {
            _humanSymbol = Symbol.O;
            _aISymbol = Symbol.X;
            _currentSymbol = _humanSymbol;
            _selectionPanel.SetActive(false);
            SetSymbolIndicators();
        }

        /// <summary>
        /// Method referenced by the restart button in the editor,
        /// used to reset the game to the beginning.
        /// </summary>
        [UsedImplicitly]
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
        [UsedImplicitly]
        public void ResetScore()
        {
            _humanScore = 0;
            _aIScore = 0;
            SaveScore();
            UpdateScores();
        }

        [UsedImplicitly]
        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        #endregion
    }
}
