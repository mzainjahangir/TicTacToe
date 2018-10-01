///////////////////////////////////////////////////////////////
//
// GridSpace (c) 2018 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/29/2018
//
///////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Custom.UI
{
    /// <summary>
    /// Class responsible for handling components of a single space on the board.
    /// </summary>
    public class GridSpace : CustomBehaviour, IPointerClickHandler
    {
        public string MoveText
        {
            get { return _moveText.text; }
            set { _moveText.text = value; }
        }

        [SerializeField, ValueRequired] private Text _moveText;

        public event EventHandler<SpaceEventArgs> Selected;

        protected virtual void Start()
        {
            MoveText = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelected(new SpaceEventArgs(this));
            Disable();
        }

        protected virtual void OnSelected(SpaceEventArgs e)
        {
            var handler = Selected;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Method to disable the space.
        /// </summary>
        public void Disable()
        {
            _moveText.raycastTarget = false;
        }
    }

    /// <summary>
    /// Class responsible for passing along the space data through events.
    /// </summary>
    public class SpaceEventArgs : EventArgs
    {
        public readonly GridSpace CurrentGridSpace;

        public SpaceEventArgs(GridSpace currentGridSpace)
        {
            CurrentGridSpace = currentGridSpace;
        }
    }
}
