package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

import java.util.regex.Matcher;

class ChangeText implements IActionParser {

    public ChangeText(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        final Matcher m = new PatternCompiler(Name()).Digit().Everything().CreateMatcher(action);
        if (m.matches() && m.groupCount() == 2) {
            return _commands.ChangeText(
                    Integer.parseInt(m.group(1)),
                    m.group(2)
            );
        }
        return null;
    }

    @Override
    public String Name() {
        return "ChangeText";
    }

    @Override
    public String Help() {
        return "Change paragraph at <position>: " + Name() + " <pos> <text>";
    }

    private final IMenuCommandsFactory _commands;
}
