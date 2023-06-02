using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strategy
{
    public abstract class BaseExecutor : MonoBehaviour, ICommandExecutor, ISelectable
    {
        [SerializeField] private AssetContext _context;

        private Dictionary<Type, ICommandExecutor> _executorsByType;
        public AssetContext Context { get => _context; protected set { _context = value; } }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Health { get; set; }
        [field: SerializeField] public int MaxHealth { get; private set; }

        public IEnumerable<Type> CommandTypes { get => GetCommandTypes(); }

        private IEnumerable<Type> GetCommandTypes()
        {
            if (_executorsByType == null)
            {
                _executorsByType = new Dictionary<Type, ICommandExecutor>();

                foreach (var t in transform.GetComponents<ICommandExecutor>())
                    if (t.CommandTypes.FirstOrDefault() != null) _executorsByType.Add(t.CommandTypes.First(), t);
            }
            return _executorsByType.Keys;
        }

        public void Execute(ICommand command)
        {
            if (command == null) return;

            var executor = _executorsByType
                .Where(kvp => kvp.Key.IsAssignableFrom(command.GetType()))
                .First().Value;
            executor?.Execute(_context.Inject(command));
            
            OnExecute(command);
        }

        protected virtual void OnExecute(ICommand command)
        {
        }
    }
}