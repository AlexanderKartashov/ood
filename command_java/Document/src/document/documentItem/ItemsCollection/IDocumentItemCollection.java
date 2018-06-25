package document.documentItem.ItemsCollection;

import java.util.Iterator;

public interface IDocumentItemCollection {

    Iterator<IDocumentItem> GetIterator();

    int GetSize();

    IDocumentItem GetItem(int position);
}
