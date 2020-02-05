package com.company;


import java.util.*;

public class Main {

    public static void main(String[] args) {
        String fileName = System.getProperty("user.dir")+"\\RecoursiveSearchEngine\\src\\com\\company";
        Reader reader = new Reader(fileName);
        MapSaver saver = new MapSaver(reader.getTextFromFiles());
        Map<String, List<String>> neighbourMap = saver.getMapWithAllData2();

        System.out.println(neighbourMap);
        MapImageGenerator generator = new MapImageGenerator(neighbourMap);
        generator.toPNG();
        System.out.println(fileName);


        String[]pngsPath=new String [3];
        pngsPath[0]="test.png";             //do podmiany nazwy png przekazywane do Frame jako sciezki do pliku
        pngsPath[1]="pepedab.png";
        pngsPath[2]="pepus.png";

        new Frame(pngsPath);
    }
}
