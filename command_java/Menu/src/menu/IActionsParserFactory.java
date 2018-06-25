package menu;

@FunctionalInterface
public interface IActionsParserFactory {
    IActionsParser CreateActionsParser();
}
