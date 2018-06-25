package fileSystem;

import environment.fileSystem.IDirectoryOperations;

import java.io.File;

class DirectoryOperations implements IDirectoryOperations {
    @Override
    public void Create(String path) {
        if (!new File(path).mkdir())
        {
            throw new IllegalArgumentException("Directory " + path + " creation failed");
        }
    }

    @Override
    public void Delete(String path) {
        final File dir = new File(path);
        if (dir.exists() && dir.isDirectory())
        {
            for (File file : dir.listFiles())
            {
                file.delete();
            }
            dir.delete();
        }
    }
}
