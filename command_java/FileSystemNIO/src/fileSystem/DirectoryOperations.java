package fileSystem;

import environment.fileSystem.IDirectoryOperations;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

class DirectoryOperations implements IDirectoryOperations {
    @Override
    public void Create(String path) throws IOException {
        Files.createDirectory(Paths.get(path));
    }

    @Override
    public void Delete(String path) throws IOException {
        Files.walk(Paths.get(path))
                .map(Path::toFile)
                .sorted((o1, o2) -> -o1.compareTo(o2))
                .forEach(File::delete);
    }
}
