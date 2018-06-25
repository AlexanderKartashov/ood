package menu.action;

import document.IMutableDocument;
import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.IDocumentItemFactory;
import document.documentItem.image.Size;
import environment.ITextEncoder;
import environment.fileSystem.IFileSystem;
import hsitory.IHistory;
import menu.IAction;
import menu.ILogger;
import menu.ISupportedActions;
import menu.action.details.*;

import java.util.Optional;

public class MenuCommandsFactory implements IMenuCommandsFactory {

    public MenuCommandsFactory(ITextEncoder encoder, IFileSystem fs, IDocumentItemFactory itemFactory, ILogger logger,
                               ICommandsFactory commands, IMutableDocument document, IHistory history, ISupportedActions supportedActions) {
        _encoder = encoder;
        _fs = fs;
        _itemFactory = itemFactory;
        _logger = logger;
        _commands = commands;
        _document = document;
        _history = history;
        _supportedActions = supportedActions;
    }

    public IAction Redo() {
        return new Redo(_history);
    }

    public IAction Undo() {
        return new Undo(_history);
    }

    public IAction List() {
        return new List(_document, _logger);
    }

    public IAction ChangeText(int pos, String text) {
        return new ChangeText(_history, _commands, _document.MutableItems(), pos, text);
    }

    @Override
    public IAction Delete(int pos) {
        return new DeleteItem(_history, _document.MutableItems(), pos, _commands);
    }

    @Override
    public IAction InsertImage(Optional<Integer> pos, Size size, String path) {
        return new InsertImage(_history, pos, size, path, _itemFactory, _commands, _document.MutableItems());
    }

    @Override
    public IAction InsertParagraph(Optional<Integer> pos, String text) {
        return new InsertParagraph(_history, pos, text, _itemFactory, _commands, _document.MutableItems());
    }

    @Override
    public IAction ResizeImage(int pos, Size size) {
        return new ResizeImage(_history, _commands, _document.MutableItems(), pos, size);
    }

    @Override
    public IAction Save(String path) {
        return new Save(path, _document, _fs, _encoder);
    }

    @Override
    public IAction SetTitle(String title) {
        return new SetTitle(title, _document.MutableTitle(), _history, _commands);
    }

    @Override
    public IAction Help() {
        return new Help(_logger, _supportedActions);
    }

    private final ITextEncoder _encoder;
    private final IFileSystem _fs;
    private final IDocumentItemFactory _itemFactory;
    private final ILogger _logger;
    private final ICommandsFactory _commands;
    private final IMutableDocument _document;
    private final IHistory _history;
    private final ISupportedActions _supportedActions;
}
