package fileSystem;

import environment.fileSystem.IFileSystem;
import environment.fileSystem.IFileSystemCreator;

public class FileSystemCreator implements IFileSystemCreator {
    @Override
    public IFileSystem CreateFileSystem() {
        return new FileSystem();
    }
}
