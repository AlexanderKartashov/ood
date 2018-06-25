package document.documentItem.ItemsCollection;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Optional;
import java.util.function.BiFunction;

public class DocumentItemCollection implements IMutableDocumentItemCollection {

    @Override
    public void InsertItem(IDocumentItem item, int position) {
        CheckPosition(position, (pos, size) -> pos <= size, "Item insertion failed");
        _documentItems.add(position, item);
    }

    @Override
    public void DeleteItem(int position) {
        CheckPosition(position, (pos, size) -> pos < size, "Item deletion failed");
        _documentItems.remove(position);
    }

    @Override
    public Iterator<IDocumentItem> GetIterator() {
        return _documentItems.iterator();
    }

    @Override
    public int GetSize() {
        return _documentItems.size();
    }

    @Override
    public IDocumentItem GetItem(int position) {
        CheckPosition(position, (pos, size) -> pos < size, "Item getter failed");
        return _documentItems.get(position);
    }

    private void CheckPosition(int position, BiFunction<Integer, Integer, Boolean> checker, String error) {
        if (!checker.apply(position, _documentItems.size()))
        {
            throw new ArrayIndexOutOfBoundsException(error);
        }
    }

    private List<IDocumentItem> _documentItems = new ArrayList<>();
}
