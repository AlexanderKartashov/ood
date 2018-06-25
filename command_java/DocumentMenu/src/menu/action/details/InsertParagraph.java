package menu.action.details;

import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.IDocumentItemFactory;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import hsitory.ICommand;
import hsitory.IHistory;

import java.util.Optional;

public class InsertParagraph extends ActionWithHistory {
    public InsertParagraph(IHistory history, Optional<Integer> position, String text, IDocumentItemFactory itemFactory, ICommandsFactory commands, IMutableDocumentItemCollection items) {
        super(history);
        _position = position;
        _text = text;
        _itemFactory = itemFactory;
        _commands = commands;
        _items = items;
    }

    @Override
    protected ICommand CreateCommand() {
        return _commands.InsertItem(_items, _itemFactory.CreateParagraph(_text), _position);
    }

    private final Optional<Integer> _position;
    private final String _text;
    private final IDocumentItemFactory _itemFactory;
    private final ICommandsFactory _commands;
    private final IMutableDocumentItemCollection _items;
}
