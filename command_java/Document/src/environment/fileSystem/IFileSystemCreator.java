package environment.fileSystem;

@FunctionalInterface
public interface IFileSystemCreator {
    IFileSystem CreateFileSystem();
}
