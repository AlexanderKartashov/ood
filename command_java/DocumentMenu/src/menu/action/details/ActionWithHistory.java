package menu.action.details;

import hsitory.ICommand;
import hsitory.IHistory;
import menu.IAction;

import java.io.IOException;

abstract class ActionWithHistory implements IAction {

    protected ActionWithHistory(IHistory history) {
        _history = history;
    }

    @Override
    public void Perform() throws Exception {

        _history.AddCommand(CreateAndExecuteCommand());
    }

    private ICommand CreateAndExecuteCommand() throws IOException {
        final ICommand command = CreateCommand();
        command.Execute();
        return command;
    }

    protected abstract ICommand CreateCommand() throws IOException;

    private final IHistory _history;
}
