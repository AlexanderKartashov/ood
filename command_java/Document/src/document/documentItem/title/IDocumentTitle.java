package document.documentItem.title;

import document.documentItem.IDocumentItemVisitor;

import java.io.IOException;

public interface IDocumentTitle {

    String GetTitle();

    void Accept(IDocumentItemVisitor visitor) throws IOException;
}
