package fileSystem;

import environment.fileSystem.IFile;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.PrintStream;

class File implements IFile {

    public File(String path) throws FileNotFoundException {
        _file = new PrintStream(new FileOutputStream(path));
    }

    @Override
    public void addString(String value) {
        _file.println(value);
    }

    @Override
    public void close() throws Exception {
        _file.close();
    }

    private final PrintStream _file;
}
