package storage;

import environment.fileSystem.IFileSystem;

public class ResourcesFactory implements IResourcesFactory{
    @Override
    public IResource CreateResource(String name, String path, IFileStorage storage) {
        return new Resource(name, storage, path);
    }
}
