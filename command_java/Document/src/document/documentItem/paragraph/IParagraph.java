package document.documentItem.paragraph;

import document.documentItem.IDocumentItemVisitor;
import document.documentItem.ItemsCollection.IDocumentItem;

public interface IParagraph extends IDocumentItem {
    String GetText();
}
