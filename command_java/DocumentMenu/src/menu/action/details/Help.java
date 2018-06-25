package menu.action.details;

import menu.IAction;
import menu.IActionsParser;
import menu.ILogger;
import menu.ISupportedActions;

public class Help implements IAction {

    public Help(ILogger logger, ISupportedActions actions){
        _logger = logger;
        _actions = actions;
    }

    @Override
    public void Perform() {
        _actions.SupportedActions().forEach((actionInfo) -> _logger.Log(actionInfo.Help()));
    }

    private final ILogger _logger;
    private final ISupportedActions _actions;
}
