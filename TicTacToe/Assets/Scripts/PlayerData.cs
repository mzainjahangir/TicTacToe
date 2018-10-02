///////////////////////////////////////////////////////////////
//
// PlayerData (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 10/1/2018
//
///////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

namespace Custom.UI
{
    /// <summary>
    /// Class responsible for components of player stats display.
    /// </summary>
    public class PlayerData : CustomBehaviour
    {
        #region References

        [SerializeField, ValueRequired] private Text _symbolIndicator;
        [SerializeField, ValueRequired] private Text _scoreText;
        [SerializeField, ValueRequired] private GameObject _turnIndicator;

        #endregion

        #region Public Methods

        /// <summary>
        /// Turns the indicator on/off based upon symbolPassed.
        /// </summary>
        /// <param name="currentSymbol">The current symbol that needs to make the move.</param>
        public void SetTurnIndicator(Symbol currentSymbol)
        {
            _turnIndicator.SetActive(_symbolIndicator.text.Contains(currentSymbol.ToString()));
        }

        /// <summary>
        /// Updates the score text.
        /// </summary>
        /// <param name="score">The score to set.</param>
        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        /// <summary>
        /// Updates the symbol text.
        /// </summary>
        /// <param name="symbol">The symbol to set.</param>
        public void SetSymbol(Symbol symbol)
        {
            _symbolIndicator.text = string.Format("[ {0} ]", symbol);
        }

        #endregion
    }
}
