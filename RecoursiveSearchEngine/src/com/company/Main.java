package com.company;


import java.util.*;

public class Main {

    public static void main(String[] args) {
        String fileName = System.getProperty("user.dir")+"\\RecoursiveSearchEngine\\src\\com\\company";
        String test = "chuj dupa cycki ,,,,, . , . ,    spierdalaj      chuju";
        System.out.println(fileName);
        Reader reader = new Reader(fileName);
   //     reader.getTextFromFiles();
       reader.getThroughFiles(fileName);
     //   reader.trimMapToNewOneWithStrings();
        reader.methodForStory1();
    //    reader.show();

        reader.methodForStory1();




       // MapImageGenerator generator;

        MethodCounter methodCounter = new MethodCounter(reader.getTextFromFiles());
      // System.out.println(methodCounter.getMethodMap());
        methodCounter.countCalls2();


//        methodCounter.getMethodsForClasses();
//        System.out.println("=========================");
//        methodCounter.show();

//        generator=new MapImageGenerator("test2",methodCounter.getMethodsInClassMap(),methodCounter.getMethodCounter());
//        generator.toPNG();
//
//
//        MapSaver saver = new MapSaver(reader.getTextFromFiles());
//        Map<String, List<String>> neighbourMap = saver.getMapWithAllData();
//
//        System.out.println(neighbourMap);
//        generator = new MapImageGenerator("test",neighbourMap);
//        generator.toPNG();
//
//
//
//        String[]pngsPath=new String [3];
//        pngsPath[0]="test.png";             //do podmiany nazwy png przekazywane do Frame jako sciezki do pliku
//        pngsPath[1]="test2.png";
//        pngsPath[2]="test.png";
//
//     Frame frame=new Frame(pngsPath);
    }
}
