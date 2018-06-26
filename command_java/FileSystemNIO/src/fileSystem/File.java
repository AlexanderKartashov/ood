package fileSystem;

import environment.fileSystem.IFile;

import java.io.BufferedWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

class File implements IFile {

    public File(String path) throws IOException {
        _file = Files.newBufferedWriter(Paths.get(path));
    }

    @Override
    public void addString(String value) throws IOException {
        _file.write(value);
        _file.newLine();
    }

    @Override
    public void close() throws Exception {
        _file.close();
    }

    private final BufferedWriter _file;
}
