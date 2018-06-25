package document.documentItem.title;

import document.documentItem.IDocumentItemVisitor;

public interface IDocumentTitle {

    String GetTitle();

    void Accept(IDocumentItemVisitor visitor);
}
