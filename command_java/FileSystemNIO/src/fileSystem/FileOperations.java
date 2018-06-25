package fileSystem;

import environment.fileSystem.IFileOperations;

import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Paths;

class FileOperations implements IFileOperations {
    @Override
    public void Copy(String from, String to) throws IOException {
        Files.copy(Paths.get(from), Paths.get(to));
    }

    @Override
    public void Delete(String path) throws IOException {
        Files.deleteIfExists(Paths.get(path));
    }
}
