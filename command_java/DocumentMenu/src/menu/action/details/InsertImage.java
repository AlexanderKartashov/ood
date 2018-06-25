package menu.action.details;

import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.IDocumentItemFactory;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import document.documentItem.image.Size;
import hsitory.ICommand;
import hsitory.IHistory;

import java.io.IOException;
import java.util.Optional;

public class InsertImage extends ActionWithHistory {
    public InsertImage(IHistory history, Optional<Integer> position, Size size, String path, IDocumentItemFactory itemFactory, ICommandsFactory commands, IMutableDocumentItemCollection items) {
        super(history);
        _position = position;
        _size = size;
        _path = path;
        _itemFactory = itemFactory;
        _commands = commands;
        _items = items;
    }

    @Override
    protected ICommand CreateCommand() throws IOException {
        return _commands.InsertItem(_items, _itemFactory.CreateImage(_path, _size), _position);
    }

    private final Optional<Integer> _position;
    private final Size _size;
    private final String _path;
    private final IDocumentItemFactory _itemFactory;
    private final ICommandsFactory _commands;
    private final IMutableDocumentItemCollection _items;
}
