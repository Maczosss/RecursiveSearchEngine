package com.company;


public class Main {

    public static void main(String[] args) {
        String fileName = System.getProperty("user.dir")+"\\RecoursiveSearchEngine\\src\\com\\company";
        MapImageGenerator generator;

        OurReader reader = new OurReader(fileName);
        reader.getThroughFilesFromAbsoluteRoot(fileName);
        reader.methodForStory1();
        generator=new MapImageGenerator("test1",reader.getMapForStory1(),null);
        generator.weightlessToPNG();




        MethodCounter methodCounter = new MethodCounter(reader.getTextFromFiles());
        //System.out.println(methodCounter.getMethodMap());
        methodCounter.countCalls();


        //methodCounter.getMethodsForClasses();
        //System.out.println("=========================");
        //methodCounter.show();
        generator=new MapImageGenerator("test2",methodCounter.getMethodCalls(),null);
        generator.weightedToPNG();


        //MapSaver saver = new MapSaver(reader.getTextFromFiles());
        //Map<String, List<String>> neighbourMap = saver.getMapWithAllData();
        //System.out.println(neighbourMap);
        //generator = new MapImageGenerator("test",neighbourMap);
        //generator.weightlessToPNG();



       String[]pngsPath=new String [3];
       pngsPath[0]="test1.png";             //do podmiany nazwy png przekazywane do Frame jako sciezki do pliku
       pngsPath[1]="test2.png";
       pngsPath[2]="test3.png";
       Frame frame=new Frame(pngsPath);
    }
}
