package document.commands;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import hsitory.AbstractCommand;

import java.util.Optional;

class DeleteItem extends AbstractCommand {

    public DeleteItem(IMutableDocumentItemCollection items, int position) {
        _oldItem = items.GetItem(position);
        _position = position;
        _items = items;
    }

    @Override
    protected void DestroyImpl() throws Exception {
        if (IsExecuted()){
            _oldItem.close();
        }
    }

    @Override
    protected void UnExecuteImpl() {
        _items.InsertItem(_oldItem, _position);
    }

    @Override
    protected void ExecuteImpl() {
        _items.DeleteItem(_position);
    }

    private final int _position;
    private final IDocumentItem _oldItem;
    private final IMutableDocumentItemCollection _items;
}
