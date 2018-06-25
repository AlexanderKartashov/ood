package hsitory;

import java.util.ArrayList;
import java.util.List;

public class History implements IHistory, AutoCloseable {

    public History(int historyLength) {
        _historyLength = historyLength;
    }

    @Override
    public void AddCommand(ICommand command) throws Exception {
        if (!command.IsExecuted())
        {
            throw new IllegalArgumentException("Command must be already executed");
        }

        RemoveUnExecutedCommands();
        RemoveOldCommands();
        AddNewCommand(command);
    }

    @Override
    public void Undo() {
        if (!CanUndo())
        {
            throw new IllegalStateException("Nothing to undo");
        }

        _commands.get(_executedCommandIndex--).UnExecute();
    }

    @Override
    public void Redo() {
        if (!CanRedo())
        {
            throw new IllegalStateException("Nothing to redo");
        }

        _commands.get(++_executedCommandIndex).Execute();
    }

    @Override
    public void close() throws Exception {
        for(ICommand command: _commands)
        {
            command.Destroy();
        }
    }

    private void AddNewCommand(ICommand command) {
        _commands.add(command);
        _executedCommandIndex = _commands.size() - 1;
    }

    private void RemoveOldCommands() throws Exception {
        if (_commands.size() == _historyLength)
        {
            _commands.get(0).Destroy();
            _commands.remove(0);
        }
    }
    private void RemoveUnExecutedCommands() throws Exception {
        if (CanRedo())
        {
            for (int i = _commands.size() - 1; i > _executedCommandIndex; --i)
            {
                _commands.get(i).Destroy();
                _commands.remove(i);
            }
        }
    }

    private boolean CanUndo() {
        return _commands.size() != 0 && _executedCommandIndex > -1;
    }

    private boolean CanRedo() {
        return _commands.size() != 0 && _executedCommandIndex < _commands.size() - 1;
    }

    private  int _executedCommandIndex = -1;
    private int _historyLength = 10;
    private final List<ICommand> _commands = new ArrayList<>();
}
