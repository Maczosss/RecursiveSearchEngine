package com.company;

import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class MethodCounter {
    private Map<String, List<String>> methodCalls = new HashMap<>();
    private ArrayList<String> dataList;
    private Map<String, Integer> methodCallsCounter = new HashMap<>();
    private ArrayList<String>declarationList;

    public MethodCounter(ArrayList<String> dataList) {
        this.dataList = dataList;
    }




    public void countCalls(){

        final Pattern regexFuncDeclar = Pattern.compile("(public|protected|private|static|\\s) +[\\w\\<\\>\\[\\]]+\\s+(\\w+) *\\([^\\)]*\\) *(\\{?|[^;])");
        //regex znajdujacy deklaracje funkcji

       // final Pattern regexFuncCalls = Pattern.compile("(?!\\bif\\b|\\bfor\\b|\\bwhile\\b|\\bswitch\\b|\\btry\\b|\\bcatch\\b)(\\b[\\w]+\\b)[\\s\\n\\r]*(?=\\(.*\\))");
        //regex dzialajacy na kazde wywolanie funkcji.


        final Pattern regexFuncCalls = Pattern.compile("(\\.[\\s\\n\\r]*[\\w]+)[\\s\\n\\r]*(?=\\(.*\\))");
        //regex dzialajacy na kazde wywolanie funkcji.
        //mniej przepuszcza


        for (int i =0;i< dataList.size();i++) {
            String loadedString=dataList.get(i);
            Matcher matcher = regexFuncDeclar.matcher(loadedString);
            if(loadedString.contains("//"))
                continue;
            if (matcher.find()) {
                List<String> methodsCallsAfter = new ArrayList<>();
                methodCalls.put(loadedString, methodsCallsAfter);

                int counter = 0;
                if(loadedString.contains("{"))
                   counter++;

                for (i+=1;i<dataList.size();i++) {
                    String secondLoadedString=dataList.get(i);



                    if (secondLoadedString.contains("{"))
                        counter++;
                    if (secondLoadedString.contains("}"))
                        counter--;
                    Matcher matcher2 = regexFuncCalls.matcher(secondLoadedString);
                    if (matcher2.find()) {

                    //    secondLoadedString.replaceAll("^[ \t]+|[ \t]+$", "");
                  //      secondLoadedString.replace(" ","");
                   //     secondLoadedString.replace("\t","");
                   //     secondLoadedString.replace("\n","");
                   //     secondLoadedString.replace("if ","");

                    //    secondLoadedString=secondLoadedString.replace("if","");
                        secondLoadedString=secondLoadedString.trim();

                        int poz1=secondLoadedString.lastIndexOf(")");
                        System.out.println(poz1);

                        System.out.println(secondLoadedString);
                        methodsCallsAfter.add(secondLoadedString);


                    }
                    //System.out.println(counter);

                    if (counter == 0 || secondLoadedString.contains("endfile")) {
                      //  System.out.println("ucieka do 1 fora");
                        break;
                    }
                }
            }

        }

       // show();
    }


    public void countCalls2(){

         declarationList=new ArrayList<String>();
        final Pattern regexFuncDeclar = Pattern.compile("(public|protected|private|static|\\s) +[\\w\\<\\>\\[\\]]+\\s+(\\w+) *\\([^\\)]*\\) *(\\{?|[^;])");
        //regex znajdujacy deklaracje funkcji

        // final Pattern regexFuncCalls = Pattern.compile("(?!\\bif\\b|\\bfor\\b|\\bwhile\\b|\\bswitch\\b|\\btry\\b|\\bcatch\\b)(\\b[\\w]+\\b)[\\s\\n\\r]*(?=\\(.*\\))");
        //regex dzialajacy na kazde wywolanie funkcji.


        for(int i=0;i<dataList.size();i++){
            String loadedString=dataList.get(i);
            Matcher matcher= regexFuncDeclar.matcher(loadedString);
            if(matcher.find()){
                if(loadedString.contains("else if")) {
                    continue;
                }
                int position = loadedString.indexOf("(");
                 String temp= loadedString.substring(0,position);
                 temp+="(";
                 temp=temp.trim();
                                                    //skrobanie stringa i usuwanie niepotrzebnych rzeczy

                 position=temp.indexOf(' ');
                while(position>0) {
                    temp = temp.substring(position + 1);
                    position = temp.indexOf(' ');
                }
                temp=temp.trim();

                declarationList.add(temp);
                //znajduje swoje deklaracje funckji i tworze ich liste
            }
        }

        System.out.println(declarationList);
        System.out.println(declarationList.size());


        final Pattern regexFuncCalls = Pattern.compile("(\\.[\\s\\n\\r]*[\\w]+)[\\s\\n\\r]*(?=\\(.*\\))");
        //regex dzialajacy na kazde wywolanie funkcji.
        //mniej przepuszcza


        for (int i =0;i< dataList.size();i++) {
            String loadedString=dataList.get(i);
            Matcher matcher = regexFuncDeclar.matcher(loadedString);
            if(loadedString.contains("//"))
                continue;
            if (matcher.find()) {                           //match do deklaracji
                List<String> methodsCallsAfter = new ArrayList<>();
                methodCalls.put(loadedString, methodsCallsAfter);
                                                                //tworze mape
                int counter = 0;
                if(loadedString.contains("{"))
                    counter++;
                                                        //for scopea deklaracji metody.
                for (i+=1;i<dataList.size();i++) {
                    String secondLoadedString=dataList.get(i);

                    if (secondLoadedString.contains("{"))
                        counter++;
                    if (secondLoadedString.contains("}"))
                        counter--;

                    Matcher matcher2 = regexFuncCalls.matcher(secondLoadedString);

                    if (matcher2.find()) {
                        //znalezienie matcha wywolanie funkcji = sprawdzanie z lista deklaracji naszych funkcji
                        for(int j=0;j<declarationList.size();j++){
                                                                        //dodawania do mapy
                            if(secondLoadedString.contains(declarationList.get(j))){

                                if(secondLoadedString.contains("//")){
                                    continue;
                                }
                                int position = secondLoadedString.indexOf("(");
                                String temp= secondLoadedString.substring(0,position);
                                temp+="(";
                                temp=temp.trim();

                                position=temp.indexOf(' ');
                                while(position>=0) {
                                    temp = temp.substring(position + 1);
                                    position = temp.indexOf(' ');
                                }

                                //skrobanie stringa do skonczenia


                                String result=temp.trim();
                                methodsCallsAfter.add(secondLoadedString);
                            }


                        }






                    }
                    //System.out.println(counter);

                    if (counter == 0 || secondLoadedString.contains("endfile")) {
                        //  System.out.println("ucieka do 1 fora");
                        break;
                    }
                }
            }

        }

        System.out.println("\n\n\n\n");

        show();

    }




    public Map<String, List<String>> getMethodMap() {
        List<String> methodsNamesInClass = new ArrayList<>();
        Map<String, List<String>> methodsInClassMap = new HashMap<>();

        int lineCounterForFile = 0;
        String temporaryClassName = "";
        for (String s : dataList) {
            if (!s.contains("File end")) {
                if (s.contains("class ") && !s.contains("(")) {
                    String line = s.strip();
                    int counterBegin = line.lastIndexOf("ss") + 2;
                    int counterEnd = line.lastIndexOf("{");
                    temporaryClassName = line.substring(counterBegin, counterEnd).strip();
                }
                if (s.contains("(") && s.contains(")") && s.contains("{") && !s.contains(";")
                        && !s.contains("for") && !s.contains("if") && !s.contains("while") && !s.contains("catch")) {
                    int counterEnd = s.lastIndexOf("(");
                    String temp = s.substring(0, counterEnd).strip();
                    String[] result;
                    result = temp.split("[ ]");
                    methodsNamesInClass.add(result[result.length - 1]);
                }
                lineCounterForFile++;
            } else if (!s.contains("(")) {
                methodsInClassMap.put(temporaryClassName, new LinkedList<>(methodsNamesInClass));
                temporaryClassName = "";
                methodsNamesInClass.clear();
                lineCounterForFile = 0;
            }
        }
        this.methodCalls = methodsInClassMap;
        System.out.println(methodsInClassMap);
        return methodsInClassMap;
    }
    public void getMethodsForClasses() {
        if(methodCallsCounter.isEmpty()){
            getMethodMap();
        }
        String oneClass = "";
        List<String> classes = new LinkedList<>();
        Map<String, String> wholeClassesWithData = new HashMap<>();
        String className = "";

        for (String s : dataList) {
            if (!s.contains("File end")) {
                oneClass += s;
                if (s.contains("class ") && !s.contains("(")) {
                    String line = s.strip();
                    int counterBegin = line.lastIndexOf("ss") + 2;
                    int counterEnd = line.lastIndexOf("{");
                    className = line.substring(counterBegin, counterEnd).strip();
                    classes.add(className);
                }

            } else if (!s.contains("(")) {
                wholeClassesWithData.put(className, oneClass);
                oneClass = "";
            }
        }
        for (String temp : classes) {
            for (String checkedMethod : methodCalls.get(temp)) {

                String str = wholeClassesWithData.get(temp);

                String strFind = checkedMethod;
                int count = 0, fromIndex = 0;

                while ((fromIndex = str.indexOf(strFind, fromIndex)) != -1) {
                    count++;
                    fromIndex++;
                }
                //methodCounter.put("Method: " + checkedMethod + " is called in class: " + temp, count);
                methodCallsCounter.put(checkedMethod, count);
            }
        }
        System.out.println(methodCallsCounter);
    }



    public void show() {



        Iterator<Map.Entry<String, List<String>>> entries = methodCalls.entrySet().iterator();
        while (entries.hasNext()) {
            Map.Entry<String, List<String>> entry = entries.next();
            System.out.println("Key = " + entry.getKey() + ", Value = \n");

            List<String> list=entry.getValue();
            for(String s : list)
                System.out.println(s);
        }
    }
    public Map<String, List<String>> getMethodCalls() {
        return methodCalls;
    }

    public Map<String, Integer> getMethodCallsCounter() {
        return methodCallsCounter;
    }
}