package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

import java.util.regex.Matcher;

class Save implements IActionParser {

    public Save(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        final Matcher m = new PatternCompiler(Name()).Everything().CreateMatcher(action);
        if (m.matches() && m.groupCount() == 1)
        {
            return _commands.Save(m.group(1));
        }
        return null;
    }

    @Override
    public String Name() {
        return "Save";
    }

    @Override
    public String Help() {
        return "Save document to <path>: " + Name() + " <path>";
    }

    private final IMenuCommandsFactory _commands;
}
