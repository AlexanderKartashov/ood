package document.commands;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import hsitory.AbstractCommand;

import java.util.Optional;

class InsertItem extends AbstractCommand {

    public InsertItem(IMutableDocumentItemCollection items, IDocumentItem newItem, Optional<Integer> position) {
        _newItem = newItem;
        _items = items;
        _position = position.isPresent() ? position.get() : items.GetSize();
    }

    @Override
    protected void DestroyImpl() throws Exception {
        if (!IsExecuted())
        {
            _newItem.close();
        }
    }

    @Override
    protected void UnExecuteImpl() {
        _items.DeleteItem(_position);
    }

    @Override
    protected void ExecuteImpl() {
        _items.InsertItem(_newItem, _position);
    }

    private final IDocumentItem _newItem;
    private final IMutableDocumentItemCollection _items;
    private final int _position;
}
