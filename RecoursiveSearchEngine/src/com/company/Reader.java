package com.company;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.*;
import java.util.stream.Collectors;

public class Reader {
    private String fileName;
    private File file;
    private BufferedReader reader;
    private List<String> lines = new ArrayList<>();
    private List<String> results = new ArrayList<>();
    private Map<String, List<String>> filesCollection = new HashMap<>(); // <nazwa folderu< nazwa pliku, tresc>>
    private static Map<File, List<File>> directoriesAndFiles = new HashMap<>();

    private int filter = 0;


    public Reader(String fileName) {
        this.fileName = fileName;
        this.file = new File(fileName);
    }


    public void getThroughFiles(String fileName) {

        File root = new File(fileName);
        File[] list = root.listFiles();
        List<File> dirs = new LinkedList<>();
        List<File> files = new LinkedList<>();
        String temp = root.getPath();
        System.out.println(temp);
        this.filter = temp.lastIndexOf("\\");

        if (list == null) return;
        for (File f : list) {
            if (f.isDirectory()) {
                dirs.add(f);
            } else {
                if (f.toString().contains(".java"))
                    files.add(f);
            }
        }
        directoriesAndFiles.put(root, files);
        if (dirs.size() != 0) {
            for (File dir : dirs) {
                getThroughFiles(dir);
            }
        }
    }

    public void getThroughFiles(File directory) {

        File[] list = directory.listFiles();
        List<File> dirs = new LinkedList<>();
        List<File> files = new LinkedList<>();

        if (list == null) return;
        for (File f : list) {
            if (f.isDirectory()) {
                dirs.add(f);
            } else {
                if (f.toString().contains(".java"))
                    files.add(f);
            }
        }
        directoriesAndFiles.put(directory, files);
        if (dirs.size() != 0) {
            for (File dir : dirs) {
                getThroughFiles(dir);
            }
        }
    }

    public void show() {
        System.out.println(directoriesAndFiles);
    }

    public void trimMapToNewOneWithStrings() {
        for (File f : directoriesAndFiles.keySet()) {
            List<File> temporary = directoriesAndFiles.get(f);
            List<String> listOfFilesInDirectory = new LinkedList<>();
            for (File temp : temporary) {
                if (temp.getName().length() > filter) {
                    listOfFilesInDirectory.add(temp.getName().substring(0, filter));
                } else {
                    listOfFilesInDirectory.add(temp.getName());

                }
            }
            if (f.toString().length() > filter) {
                String name = f.getPath();
                name.substring(0, filter);
                filesCollection.put(name, listOfFilesInDirectory);
            }
            else {
                String name = f.getPath();
                filesCollection.put(name, listOfFilesInDirectory);
            }

        }

        System.out.println("================================");
        System.out.println(filesCollection);
    }


    //reforging ends

    //===================================================


    List<String> getTextFromFiles() {
        if (file.canRead()) {
            for (File f : Objects.requireNonNull(file.listFiles())) {
                try {
                    reader = new BufferedReader(new FileReader(f.getPath()));
                    lines.add(reader.lines().collect(Collectors.toList()).toString());
                } catch (FileNotFoundException e) {
                    System.out.println("File not found!" + e);
                }
            }
            for (String line : lines) {
                String[] arr = line.split(",");
                results.addAll(Arrays.asList(arr));
                results.add("File end");
            }

        }
//        for (String result : results) {
//            System.out.println(result + "\n");
//        }
        return results;
    }

    private static List<String> getFilterOutput(List<String> lines, String filter) {
        List<String> result = new ArrayList<>();
        for (String line : lines) {
            if (line.contains(filter)) {
                result.add(line);
            }
        }
        return result;
    }

}
