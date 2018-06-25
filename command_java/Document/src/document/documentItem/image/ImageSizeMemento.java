package document.documentItem.image;

import hsitory.IMemento;

public class ImageSizeMemento implements IMemento {

    public ImageSizeMemento(Size oldSize, IMutableImage image) {
        _oldSize = oldSize;
        _image = image;
    }

    @Override
    public void Restore() {
        _image.SetSize(_oldSize);
    }

    private final IMutableImage _image;
    private final Size _oldSize;
}
