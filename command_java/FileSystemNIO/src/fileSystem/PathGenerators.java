package fileSystem;

import environment.fileSystem.IPathGenerators;

import java.io.File;
import java.io.IOException;
import java.nio.file.Paths;

class PathGenerators implements IPathGenerators {
    @Override
    public String GetUniqueFileNameInDirectory(String folder) throws IOException {
        final String fileName = "resource";
        final String ext = ".tmp";
        return GenerateUniqueNameInFolder(folder, fileName, ext);
    }

    @Override
    public String GetUniqueTempFolderPath() throws IOException {
        final String fileName = "java_temp";
        final String folder = System.getProperty("java.io.tmpdir");
        return RelativeToAbsolute(folder, GenerateUniqueNameInFolder(folder, fileName, ""));
    }

    @Override
    public String RelativeToAbsolute(String root, String name) throws IOException {
        return Paths.get(root).resolve(name).toString();
    }

    @Override
    public String AbsoluteToRelative(String root, String path) {
        return Paths.get(path).relativize(Paths.get(root)).toString();
    }

    @Override
    public String CurrentModulePath() {
        //return System.getProperty("java.class.path");
        return Paths.get(".").toAbsolutePath().normalize().toString();
    }

    @Override
    public String GetFileName(String path) {
        return Paths.get(path).getFileName().toString();
    }

    @Override
    public String ReplaceExtension(String fileName, String newExtension) {
        final int index = fileName.lastIndexOf(".");
        final String ext = fileName.substring(index);
        final String stem = fileName.substring(0, index - 1);
        return stem + ext;
    }

    private String GenerateUniqueNameInFolder(String folder, String name, String ext) {
        int index = 1;
        String fileName = name + index++ + ext;
        String path = folder + fileName;

        while(new File(path).exists()) {
            fileName = name + index++ + ext;
            path = folder + fileName;
        }
        return fileName;
    }

}
