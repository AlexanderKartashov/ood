package storage;

import java.io.IOException;

public interface IFileStorage extends AutoCloseable{

    void Delete(String name) throws IOException;

    IResource Add(String path) throws IOException;
}
