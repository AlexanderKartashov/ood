package document.documentItem;

import document.documentItem.image.IImage;
import document.documentItem.paragraph.IParagraph;
import document.documentItem.title.IDocumentTitle;

import java.io.IOException;

public interface IDocumentItemVisitor {

    void Visit(IDocumentTitle title) throws IOException;

    void Visit(IParagraph paragraph) throws IOException;

    void Visit(IImage image) throws IOException;
}
