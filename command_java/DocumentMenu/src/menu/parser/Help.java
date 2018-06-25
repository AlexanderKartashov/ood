package menu.parser;


import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

class Help implements IActionParser {
    public Help(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        return new PatternCompiler(Name()).CreateMatcher(action).matches() ? _commands.Help() : null;
    }

    @Override
    public String Name() {
        return "Help";
    }

    @Override
    public String Help() {
        return Name();
    }

    private final IMenuCommandsFactory _commands;
}
