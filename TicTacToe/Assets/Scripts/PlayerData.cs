///////////////////////////////////////////////////////////////
//
// PlayerData (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 10/1/2018
//
///////////////////////////////////////////////////////////////

using Custom.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.UI
{
    /// <summary>
    /// Class responsible for components of player stats display.
    /// </summary>
    public class PlayerData : CustomBehaviour
    {
        [SerializeField, ValueRequired] private Text _symbolIndicator;
        [SerializeField, ValueRequired] private Text _scoreText;
        [SerializeField, ValueRequired] private GameObject _turnIndicator;

        public void SetIndicator(Symbol currentSymbol)
        {
            if (_symbolIndicator.text.Contains(currentSymbol.ToString()))
            {
                _turnIndicator.SetActive(true);
            }
            else
            {
                _turnIndicator.SetActive(false);
            }
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
