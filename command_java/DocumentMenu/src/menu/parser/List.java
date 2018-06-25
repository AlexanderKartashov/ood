package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

class List implements IActionParser {

    public List(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        return new PatternCompiler(Name()).CreateMatcher(action).matches() ? _commands.List() : null;
    }

    @Override
    public String Name() {
        return "List";
    }

    @Override
    public String Help() {
        return "Lists all items: " + Name();
    }

    private final IMenuCommandsFactory _commands;
}
