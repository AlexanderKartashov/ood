package environment.fileSystem;

import java.io.IOException;

public interface IFileOperations {

    void Copy(String from, String to) throws IOException;

    void Delete(String path) throws IOException;

    IFile CreateFile(String path) throws IOException;
}
