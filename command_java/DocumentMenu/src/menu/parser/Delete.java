package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

import java.util.regex.Matcher;

class Delete implements IActionParser {
    public Delete(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        final Matcher m = new PatternCompiler(Name()).Digit().CreateMatcher(action);
        if(m.matches() && m.groupCount() == 1) {
            return _commands.Delete(Integer.parseInt(m.group(1)));
        }
        return null;
    }

    @Override
    public String Name() {
        return "Delete";
    }

    @Override
    public String Help() {
        return "Delete item at <position>: " + Name() + " <pos>";
    }

    private final IMenuCommandsFactory _commands;
}
