package document;

import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import document.documentItem.title.IMutableDocumentTitle;

public interface IMutableDocument extends IDocument {

    IMutableDocumentTitle MutableTitle();

    IMutableDocumentItemCollection MutableItems();
}
