package menu.parser;

import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

import java.util.Optional;
import java.util.regex.Matcher;

class InsertParagraph implements IActionParser {

    public InsertParagraph(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        final Matcher m = new PatternCompiler(Name()).Position().Everything().CreateMatcher(action);
        if (m.matches() && m.groupCount() == 2)
        {
            final Optional<Integer> pos = (m.group(1).equals("end")) ? Optional.empty() : Optional.of(Integer.parseInt(m.group(1)));
            return _commands.InsertParagraph(pos, m.group(2));
        }
        return null;
    }

    @Override
    public String Name() {
        return "InsertParagraph";
    }

    @Override
    public String Help() {
        return "Inserts paragraph in <position> with <text>: " + Name() + " <pos> <text>";
    }

    private final IMenuCommandsFactory _commands;
}
