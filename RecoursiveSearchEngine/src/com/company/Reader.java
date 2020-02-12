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
    private Map<String, List<String>> filesCollection = new HashMap<>();
    private static Map<File, List<File>> directoriesAndFiles = new HashMap<>();

    //    private Map<String, Map<String, String>> mapWithFilesContent = new HashMap<>();
    private Map<String, Map<String, List<String>>> mapWithFilesContent = new HashMap<>();

    private Map<String, List<String>> mapForStory1 = new HashMap<>();


    private int filter = 0;


    public Reader(String fileName) {
        this.fileName = fileName;
        this.file = new File(fileName);
    }

//    public Map<String,Map<String,String>> getContentFromFile

    public void getThroughFiles(String fileName) {

        File root = new File(fileName);
        File[] list = root.listFiles();
        List<File> dirs = new LinkedList<>();
        List<File> files = new LinkedList<>();
        String temp = root.getPath();
        System.out.println(temp);
        this.filter = temp.lastIndexOf("\\");
        Map<String, List<String>> fileContents = new HashMap<>();

        if (list == null) return;
        for (File f : list) {
            if (f.isDirectory()) {
                dirs.add(f);
            } else {
                if (f.toString().contains(".java")) {
                    files.add(f);
                    fileContents.putAll(getTextFromFile(f));
                }
            }
        }
        mapWithFilesContent.put(root.getName(), new HashMap<>(fileContents));
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
        Map<String, List<String>> fileContents = new HashMap<>();


        if (list == null) return;
        for (File f : list) {
            if (f.isDirectory()) {
                dirs.add(f);
            } else {
                if (f.toString().contains(".java")) {
                    files.add(f);
                    fileContents = getTextFromFile(f);
                }

            }
        }
        mapWithFilesContent.put(directory.getName(), new HashMap<>(fileContents));
        directoriesAndFiles.put(directory, files);
        if (dirs.size() != 0) {
            for (File dir : dirs) {
                getThroughFiles(dir);
            }
        }
    }

    public void show() {
        System.out.println("directoriesAndFiles collection: " + directoriesAndFiles);
        System.out.println("============================");
        System.out.println("mapWithFilesContent: " + mapWithFilesContent);

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
            } else {
                String name = f.getPath();
                filesCollection.put(name, listOfFilesInDirectory);
            }

        }

        System.out.println("================================");
        System.out.println(filesCollection);
    }

    public Map<String, List<String>> getTextFromFile(File file) {
        StringBuilder outputFromFile = new StringBuilder();
        List<String> lines = new LinkedList<>();
        BufferedReader reader;
        try {
            reader = new BufferedReader(new FileReader(file.getPath()));
            lines.add(reader.lines().collect(Collectors.toList()).toString());
        } catch (FileNotFoundException e) {
            System.out.println("File not found!" + e);
        }
        String classname = "";
        for (String temp : lines) {
            if (temp.contains("class")) {
                System.out.println("been here"+temp);
            }
            if (temp.contains("class ")) {
                String line = temp.replace(" ", "");
                int counterBegin = line.indexOf("class",0) + 5;
                int counterEnd = line.indexOf("{", 0);
                classname = line.substring(counterBegin, counterEnd);
                if (classname.contains("extends")) {
                    int end = classname.indexOf("extends", 0);
                    classname = classname.substring(0, end);
                }
                if (classname.contains("implements")) {
                    int end = classname.indexOf("implements", 0);
                    classname = classname.substring(0, end);
                }
            }
        }
        Map<String, List<String>> result = new HashMap<>();
        result.put(classname, lines);
        return result;
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
