package environment.fileSystem;

public interface IPathChecks {
    boolean PathExists(String path);

    boolean IsFile(String path);

    boolean IsDirectory(String path);

    boolean IsAbsolute(String path);
}
