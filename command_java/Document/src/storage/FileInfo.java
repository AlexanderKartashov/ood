package storage;

class FileInfo {

    FileInfo(IResource res, String path) {
        _res = res;
        _path = path;
    }

    public String GetPath() {
        return _path;
    }

    public IResource GetResource() {
        return _res;
    }

    private final IResource _res;
    private final String _path;
}
