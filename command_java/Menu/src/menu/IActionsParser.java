package menu;

public interface IActionsParser extends ISupportedActions  {
    IAction ParseAction(String action);
}
