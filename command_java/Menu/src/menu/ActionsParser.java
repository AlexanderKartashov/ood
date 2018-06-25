package menu;

import java.util.Collection;
import java.util.stream.Collectors;

public class ActionsParser implements IActionsParser{

    public ActionsParser(Collection<IActionParser> parsers) {
        _parsers = parsers;
    }

    @Override
    public IAction ParseAction(String action) {
        for (IActionParser parser : _parsers){
            IAction parsedAction = parser.Parse(action);
            if (parsedAction != null)
            {
                return parsedAction;
            }
        }

        throw new IllegalArgumentException("Unsupported action <" + action + ">");
    }

    @Override
    public Collection<IActionInfo> SupportedActions() {
        return _parsers.stream().collect(Collectors.toSet());
    }

    private final Collection<IActionParser> _parsers;
}
