package document.documentItem.image;

import hsitory.IMementoOriginator;

public interface IMutableImage extends IImage, IMementoOriginator {
    void SetSize(Size size);
}
