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
    /// Class responsible for handling operations of a single space on the board.
    /// </summary>
    public class GridSpace : CustomBehaviour, IPointerClickHandler
    {
        #region References

        [SerializeField, ValueRequired] private Text _moveText;

        #endregion

        #region Member Variables

        /// <summary>
        /// Text that displays the current symbol on the space.
        /// </summary>
        public string MoveText
        {
            get { return _moveText.text; }
            set { _moveText.text = value; }
        }

        /// <summary>
        /// The number of space, deduced from the name of the space.
        /// </summary>
        public int SpaceNumber
        {
            get { return int.Parse(transform.name); }
        }

        /// <summary>
        /// Event that is fired every time the space is selected.
        /// </summary>
        public event EventHandler<SpaceEventArgs> Selected;

        #endregion

        #region Initialization & Deconstruction

        protected override void Awake()
        {
            base.Awake();
            MoveText = null;
        }

        #endregion

        #region Event Handlers

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelected(new SpaceEventArgs(this));
        }

        protected virtual void OnSelected(SpaceEventArgs e)
        {
            var handler = Selected;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to disable the space.
        /// </summary>
        public void Disable()
        {
            _moveText.raycastTarget = false;
        }

        /// <summary>
        /// Method to Enable the space.
        /// </summary>
        public void Enable()
        {
            _moveText.raycastTarget = true;
        }

        #endregion
    }

    #region EventArg Classes

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

    #endregion
}
