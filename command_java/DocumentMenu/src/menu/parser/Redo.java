package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

class Redo implements IActionParser {

    public Redo(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        return new PatternCompiler(Name()).CreateMatcher(action).matches() ? _commands.Redo() : null;
    }

    @Override
    public String Name() {
        return "Redo";
    }

    @Override
    public String Help() {
        return "Redoes last command: " + Name();
    }

    private final IMenuCommandsFactory _commands;
}
