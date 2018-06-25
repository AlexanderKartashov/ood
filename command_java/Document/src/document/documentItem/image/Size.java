package document.documentItem.image;

public class Size {

    public Size(int w, int h) {
        _w = w;
        _h = h;
    }

    public int Width() {
        return _w;
    }

    public int Height() {
        return _h;
    }

    private final int _w;
    private final int _h;
}
