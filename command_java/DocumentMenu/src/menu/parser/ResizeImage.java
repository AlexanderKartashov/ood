package menu.parser;

import document.documentItem.image.Size;
import menu.IAction;
import menu.IActionParser;
import menu.action.IMenuCommandsFactory;

import java.util.regex.Matcher;

class ResizeImage implements IActionParser {

    public ResizeImage(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    @Override
    public IAction Parse(String action) throws IllegalArgumentException {
        final Matcher m = new PatternCompiler(Name()).Digit().Digit().Digit().CreateMatcher(action);
        if (m.matches() && m.groupCount() == 3)
        {
            return _commands.ResizeImage(Integer.parseInt(m.group(1)),
                    new Size(Integer.parseInt(m.group(2)),
                            Integer.parseInt(m.group(3)))
            );
        }
        return null;
    }

    @Override
    public String Name() {
        return "ResizeImage";
    }

    @Override
    public String Help() {
        return "Resize image at <pos> to <w> <h> :" + Name() + " <pos> <w> <h>";
    }

    private final IMenuCommandsFactory _commands;
}
