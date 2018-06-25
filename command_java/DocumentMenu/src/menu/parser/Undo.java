package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

class Undo implements IActionParser {
    public Undo(IMenuCommandsFactory commands){
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        return new PatternCompiler(Name()).CreateMatcher(action).matches() ? _commands.Undo() : null;
    }

    @Override
    public String Name() {
        return "Undo";
    }

    @Override
    public String Help() {
        return "Undoes last command: " + Name();
    }

    private final IMenuCommandsFactory _commands;
}
