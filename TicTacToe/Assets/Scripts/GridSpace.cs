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
        public int SpaceNumber { get; private set; }

        public Text MoveText
        {
            get { return _moveText; }
            set { _moveText = value; }
        }

        [SerializeField, ValueRequired] private Text _moveText;

        public event EventHandler<SpaceEventArgs> Selected;

        protected virtual void Start()
        {
            SpaceNumber = int.Parse(transform.name);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelected(new SpaceEventArgs(this));
            _moveText.raycastTarget = false;
        }

        protected virtual void OnSelected(SpaceEventArgs e)
        {
            var handler = Selected;
            if (handler != null) handler(this, e);
        }
    }

    public class SpaceEventArgs : EventArgs
    {
        public GridSpace CurrentGridSpace;

        public SpaceEventArgs(GridSpace currentGridSpace)
        {
            CurrentGridSpace = currentGridSpace;
        }
    }
}
