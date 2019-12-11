package com.company;


import java.util.*;

public class Main {

    public static void main(String[] args) {
        String fileName = System.getProperty("user.dir")+"\\RecoursiveSearchEngine\\src\\com\\company";
        Reader reader = new Reader(fileName);
        MapSaver saver = new MapSaver(reader.getTextFromFiles());
        Map<String, List<String>> neighbourMap = saver.getMapWithAllData();

        System.out.println(neighbourMap);
        MapImageGenerator generator = new MapImageGenerator(neighbourMap);
        generator.toPNG();
        System.out.println(fileName);
    }
}
