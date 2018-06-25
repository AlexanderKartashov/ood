package menu.parser;

import menu.ActionsParser;
import menu.IActionsParser;
import menu.IActionsParserFactory;
import menu.action.IMenuCommandsFactory;

import java.util.Arrays;

public class ActionParserCreator implements IActionsParserFactory {

    public ActionParserCreator(IMenuCommandsFactory commands) {
        _commands = commands;
    }

    public IActionsParser CreateActionsParser() {
        return new ActionsParser(Arrays.asList(new ChangeText(_commands),
                new Delete(_commands),
                new Help(_commands),
                new InsertImage(_commands),
                new InsertParagraph(_commands),
                new List(_commands),
                new Redo(_commands),
                new ResizeImage(_commands),
                new Save(_commands),
                new SetTitle(_commands),
                new Undo(_commands)));
    }

    private final IMenuCommandsFactory _commands;
}
