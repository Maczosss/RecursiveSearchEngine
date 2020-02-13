package com.company;


import java.util.*;

public class Main {

    public static void main(String[] args) {
        String fileName = System.getProperty("user.dir")+"\\RecoursiveSearchEngine\\src\\com\\company";
        MapImageGenerator generator;

        Reader reader = new Reader(fileName);
        reader.getThroughFiles(fileName);
        reader.methodForStory1();
        generator=new MapImageGenerator("test1",reader.getMapForStory1());
        generator.toPNG();




        MethodCounter methodCounter = new MethodCounter(reader.getTextFromFiles());
      // System.out.println(methodCounter.getMethodMap());
        methodCounter.countCalls2();


//        methodCounter.getMethodsForClasses();
//        System.out.println("=========================");
//        methodCounter.show();
        generator=new MapImageGenerator("test2",methodCounter.getMethodCalls());
        generator.toPNG();
//
//
//        MapSaver saver = new MapSaver(reader.getTextFromFiles());
//        Map<String, List<String>> neighbourMap = saver.getMapWithAllData();

//      System.out.println(neighbourMap);
//       generator = new MapImageGenerator("test",neighbourMap);
 //      generator.toPNG();
//
//
//
       String[]pngsPath=new String [3];
       pngsPath[0]="test1.png";             //do podmiany nazwy png przekazywane do Frame jako sciezki do pliku
       pngsPath[1]="test2.png";
       pngsPath[2]="test3.png";
       Frame frame=new Frame(pngsPath);
    }
}
