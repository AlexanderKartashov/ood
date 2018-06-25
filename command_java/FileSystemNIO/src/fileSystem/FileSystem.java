package fileSystem;

import environment.fileSystem.IDirectoryOperations;
import environment.fileSystem.IFileOperations;
import environment.fileSystem.IFileSystem;
import environment.fileSystem.IPathOperations;

class FileSystem implements IFileSystem {
    @Override
    public IFileOperations FileOperations() {
        return new FileOperations();
    }

    @Override
    public IPathOperations PathOperations() {
        return new PathOperations();
    }

    @Override
    public IDirectoryOperations DirectoryOperations() {
        return new DirectoryOperations();
    }
}
