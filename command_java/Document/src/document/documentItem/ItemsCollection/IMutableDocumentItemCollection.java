package document.documentItem.ItemsCollection;

import java.util.Optional;

public interface IMutableDocumentItemCollection extends IDocumentItemCollection {

    void InsertItem(IDocumentItem item, int position);

    void DeleteItem(int position);
}
