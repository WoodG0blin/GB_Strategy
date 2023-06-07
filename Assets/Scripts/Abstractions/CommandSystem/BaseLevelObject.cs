using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strategy
{
    public abstract class BaseLevelObject : MonoBehaviour, ISelectable
    {
        [SerializeField] private AssetContext _context;
        public AssetContext Context { get => _context; protected set { _context = value; } }

        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Health { get; set; }
        [field: SerializeField] public int MaxHealth { get; private set; }

        private List<ICommandExecutor> _executorsByType;
        public List<ICommandExecutor> Commands { get => GetCommandTypes(); }

        private List<ICommandExecutor> GetCommandTypes()
        {
            if (_executorsByType == null)
            {
                _executorsByType = new List<ICommandExecutor>();
                _executorsByType.AddRange(transform.GetComponents<ICommandExecutor>());
            }
            return _executorsByType;
        }
    }
}