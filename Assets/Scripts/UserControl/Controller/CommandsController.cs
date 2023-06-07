using System;
using Zenject;

namespace Strategy
{
    internal class CommandsController : IDisposable
    {
        private IControlsUIView _view;
        private ICommandsModel _model;

        public CommandsController(IControlsUIView view, ICommandsModel model)
        {
            _view = view;
            _model = model;
            _view.OnCommand += _model.OnCommandButtonClicked;
            _model.OnCommandChosen += _view.BlockCommands;
            _model.OnCommandCanceled += _view.UnBlockCommands;
            _model.OnCommandIssued += _view.UnBlockCommands;
        }

        public void Dispose()
        {
            _view.OnCommand -= _model.OnCommandButtonClicked;
            _model.OnCommandChosen -= _view.BlockCommands;
            _model.OnCommandCanceled -= _view.UnBlockCommands;
            _model.OnCommandIssued -= _view.UnBlockCommands;
            _view = null;
        }

        public void OnSelectedChanged(ISelectable selected)
        {
            _view.Clear();
            if (selected != null)
            {
                _view.SetLayout(selected.Commands);
                _model.OnSelectionChanged();
            }
        }
    }
}
