package document.documentItem.image;

import document.documentItem.IDocumentItemVisitor;
import hsitory.IMemento;
import storage.IResource;

public class Image implements IMutableImage {

    public Image(IResource resource, Size size) {
        _size = size;
        _resource = resource;
    }

    @Override
    public void close() throws Exception {
        _resource.Delete();
    }

    @Override
    public IImage GetImage() {
        return this;
    }

    @Override
    public Size GetSize() {
        return _size;
    }

    @Override
    public IResource GetResource() {
        return _resource;
    }

    @Override
    public void Accept(IDocumentItemVisitor visitor) {
        visitor.Visit(this);
    }

    @Override
    public void SetSize(Size size) {
        _size = size;
    }

    @Override
    public IMemento GetState() {
        return new ImageSizeMemento(_size, this);
    }

    private final IResource _resource;
    private Size _size;
}
