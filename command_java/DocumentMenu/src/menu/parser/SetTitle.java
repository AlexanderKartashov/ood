package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

import java.util.regex.Matcher;

class SetTitle implements IActionParser {

    public SetTitle(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        final Matcher m = new PatternCompiler(Name()).Everything().CreateMatcher(action);
        if (m.matches() && m.groupCount() == 1)
        {
            return _commands.SetTitle(m.group(1));
        }
        return null;
    }

    @Override
    public String Name() {
        return "SetTitle";
    }

    @Override
    public String Help() {
        return "Set document title: " + Name() + " <text>";
    }

    private final IMenuCommandsFactory _commands;
}
