package document.documentItem.ItemsCollection;

import document.documentItem.IDocumentItemVisitor;
import document.documentItem.image.IImage;
import document.documentItem.paragraph.IParagraph;

import java.io.IOException;

public interface IDocumentItem extends AutoCloseable {

    default IParagraph GetParagraph() {
        return null;
    }

    default IImage GetImage() {
        return null;
    }

    void Accept(IDocumentItemVisitor visitor) throws IOException;
}
