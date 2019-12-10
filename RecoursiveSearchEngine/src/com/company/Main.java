package com.company;


public class Main {

    public static void main(String[] args) {
        String fileName = "file path";
        Reader reader = new Reader(fileName);
        MapSaver saver = new MapSaver(reader.getTextFromFiles());
        System.out.println(saver.getMapWithAllData2());
    }
}
