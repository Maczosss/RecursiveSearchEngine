package com.company;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

class Reader {
    private String fileName;
    private File file;
    private BufferedReader reader;
    private List<String> lines = new ArrayList<>();
    private List<String> results = new ArrayList<>();


    Reader(String fileName) {
        this.fileName = fileName;
        this.file = new File(fileName);
    }

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
