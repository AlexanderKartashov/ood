package document.documentItem.ItemsCollection;

import document.documentItem.image.IImage;
import document.documentItem.image.Image;
import document.documentItem.image.Size;
import document.documentItem.paragraph.IParagraph;
import document.documentItem.paragraph.Paragraph;
import storage.IFileStorage;

import java.io.IOException;

public class DocumentItemFactory implements IDocumentItemFactory, AutoCloseable {

    public DocumentItemFactory(IFileStorage storage){
        _storage = storage;
    }

    @Override
    public void close() throws Exception {
        _storage.close();
    }

    @Override
    public IImage CreateImage(String path, Size size) throws IOException {
        return new Image(_storage.Add(path), size);
    }

    @Override
    public IParagraph CreateParagraph(String text) {
        return new Paragraph(text);
    }

    private final IFileStorage _storage;
}
