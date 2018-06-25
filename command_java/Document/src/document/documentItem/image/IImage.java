package document.documentItem.image;

import document.documentItem.IDocumentItemVisitor;
import document.documentItem.ItemsCollection.IDocumentItem;
import storage.IResource;

public interface IImage extends IDocumentItem {

    Size GetSize();

    IResource GetResource();
}
