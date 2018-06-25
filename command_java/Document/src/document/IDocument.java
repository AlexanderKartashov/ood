package document;

import document.documentItem.ItemsCollection.IDocumentItemCollection;
import document.documentItem.image.IMutableImage;
import document.documentItem.title.IDocumentTitle;
import document.documentItem.title.IMutableDocumentTitle;

public interface IDocument {
    IDocumentTitle Title();
    IDocumentItemCollection Items();
}
