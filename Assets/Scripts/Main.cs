using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public class Main : MonoBehaviour
    {
        private UserControlManager _inputController;

        [SerializeField] Transform _input;
        [SerializeField] Transform _levelObjectUI;
        void Start()
        {
            //_inputController = new UserControlManager(_input, _levelObjectUI);
        }

        void Update()
        {

        }
    }
}
