package document;

import document.documentItem.ItemsCollection.IDocumentItemCollection;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import document.documentItem.title.IDocumentTitle;
import document.documentItem.title.IMutableDocumentTitle;

public class Document implements IMutableDocument {
    public Document(IMutableDocumentItemCollection documentItemsCollection, IMutableDocumentTitle title) {
        _ItemsCollection = documentItemsCollection;
        _title = title;
    }

    @Override
    public IMutableDocumentTitle MutableTitle() {
        return _title;
    }

    @Override
    public IMutableDocumentItemCollection MutableItems() {
        return _ItemsCollection;
    }

    @Override
    public IDocumentTitle Title() {
        return _title;
    }

    @Override
    public IDocumentItemCollection Items() {
        return _ItemsCollection;
    }

    private final IMutableDocumentItemCollection _ItemsCollection;
    private final IMutableDocumentTitle _title;
}
