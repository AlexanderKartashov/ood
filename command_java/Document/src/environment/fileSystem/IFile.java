package environment.fileSystem;

import java.io.IOException;

public interface IFile extends AutoCloseable {
    void addString(String value) throws IOException;
}
