package environment.fileSystem;

import java.io.IOException;

public interface IDirectoryOperations {

    void Create(String path) throws IOException;

    void Delete(String path) throws IOException;
}
