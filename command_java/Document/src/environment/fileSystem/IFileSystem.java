package environment.fileSystem;

public interface IFileSystem {

    IFileOperations FileOperations();

    IPathOperations PathOperations();

    IDirectoryOperations DirectoryOperations();
}
