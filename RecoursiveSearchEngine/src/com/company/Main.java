package com.company;

import java.io.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

public class Main {

    public static void main(String[] args) {
        File file = new File("C:\\Users\\macie\\OneDrive\\Desktop\\RecoursiveSearchEngine\\src\\com\\company");
        BufferedReader reader = null;
        List<String> lines = new ArrayList<>();
        List<String> results = new ArrayList<>();

        if (file.canRead()) {
            for (File f : file.listFiles()) {
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
        for (String result : results) {
            System.out.println(result + "\n");
        }
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
