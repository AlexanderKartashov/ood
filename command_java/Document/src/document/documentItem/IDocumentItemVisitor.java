package document.documentItem;

import document.documentItem.image.IImage;
import document.documentItem.paragraph.IParagraph;
import document.documentItem.title.IDocumentTitle;

public interface IDocumentItemVisitor {

    void Visit(IDocumentTitle title);

    void Visit(IParagraph paragraph);

    void Visit(IImage image);
}
