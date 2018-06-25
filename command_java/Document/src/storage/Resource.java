package storage;

import environment.fileSystem.IFileSystem;

import java.io.IOException;

public class Resource implements IResource {

    public Resource(String name, IFileStorage storage, String path) {
        _name = name;
        _storage = storage;
        _path = path;
    }

    @Override
    public String Name() {
        return _name;
    }

    @Override
    public String Path() {
        return _path;
    }

    @Override
    public void Delete() throws IOException {
        _storage.Delete(_name);
    }

    private final String _name;
    private final IFileStorage _storage;
    private final String _path;
}
