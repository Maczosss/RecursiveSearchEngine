package com.company;


public class Main {

    public static void main(String[] args) {
        String fileName = "path to file";
        Reader reader = new Reader(fileName);
        MapSaver saver = new MapSaver(reader.getTextFromFiles());
        saver.getMapWithAllData();
    }
}
