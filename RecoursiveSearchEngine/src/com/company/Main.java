package com.company;


public class Main {

    public static void main(String[] args) {
        String fileName = System.getProperty("user.dir")+"\\RecoursiveSearchEngine\\src\\com\\company";
        MapImageGenerator generator;

        OurReader reader = new OurReader(fileName);
        reader.getThroughFilesFromAbsoluteRoot(fileName);
        reader.methodForStory1();
        generator=new MapImageGenerator("test1",reader.getMapForStory1(),null);
        generator.toPNG(false);

        MethodCounter methodCounter = new MethodCounter(reader.getTextFromFiles());
        methodCounter.countCalls();

        generator=new MapImageGenerator("test2",methodCounter.getMethodCalls(),null);
        generator.toPNG(true);

       String[]pngsPath=new String [3];
       pngsPath[0]="test1.png";             //do podmiany nazwy png przekazywane do Frame jako sciezki do pliku
       pngsPath[1]="test2.png";
       pngsPath[2]="test3.png";
       Frame frame=new Frame(pngsPath);
    }
}
