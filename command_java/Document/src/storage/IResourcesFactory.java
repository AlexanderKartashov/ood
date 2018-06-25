package storage;

public interface IResourcesFactory {
    IResource CreateResource(String name, String path, IFileStorage storage);
}
