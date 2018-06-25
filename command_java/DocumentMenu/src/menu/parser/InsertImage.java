package menu.parser;

import document.documentItem.image.Size;
import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

import java.util.Optional;
import java.util.regex.Matcher;

class InsertImage implements IActionParser {

    public InsertImage(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        final Matcher m = new PatternCompiler(Name()).Position().Digit().Digit().Everything().CreateMatcher(action);
        if (m.matches() && m.groupCount() == 4)
        {
            final Optional<Integer> pos = (m.group(1).equals("end")) ? Optional.empty() : Optional.of(Integer.parseInt(m.group(1)));
            return _commands.InsertImage(pos,
                    new Size(Integer.parseInt(m.group(2)),
                            Integer.parseInt(m.group(3))),
                    m.group(4));
        }
        return null;
    }

    @Override
    public String Name() {
        return "InsertImage";
    }

    @Override
    public String Help() {
        return "Inserts image in <position> with size <w> <h> from <path>: " + Name() + " <pos> <w> <h> <path>";
    }

    private final IMenuCommandsFactory _commands;
}
