package com.company;


public class Main {

    public static void main(String[] args) {
        String fileName = "C:\\Users\\macie\\OneDrive\\Desktop\\Gir repository\\RecursiveSearchEngine\\RecoursiveSearchEngine\\src\\com\\company";
        Reader reader = new Reader(fileName);
//        MapSaver saver = new MapSaver(reader.getTextFromFiles());
        MethodCounter methodCounter = new MethodCounter(reader.getTextFromFiles());
        System.out.println(methodCounter.getMethodMap());

        //String test = "public void method (int x){";

    }
}
