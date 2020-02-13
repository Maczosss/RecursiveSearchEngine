package com.company;

import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class MethodCounter {
    private Map<String, Map<String,Integer>> methodCalls = new HashMap<>();
    private ArrayList<String> dataList;
    private Map<String, Integer> methodCallsCounter = new HashMap<>();
    private ArrayList<String>declarationList;

    public MethodCounter(ArrayList<String> dataList) {
        this.dataList = dataList;
    }

    public void countCalls(){

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
                Map<String,Integer> methodsCallsAfter = new HashMap<>();
                for (int j = 0;j<dataList.size();j++) {
                    if(loadedString.contains(declarationList.get(j))) {
                        methodCalls.put(declarationList.get(j)+")", methodsCallsAfter);
                        break;
                    }
                }
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

                                methodsCallsAfter.put(declarationList.get(j),0);
                                //Integer temp =0;

                                methodsCallsAfter.put(declarationList.get(j),1);

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

    public void show() {



        Iterator<Map.Entry<String, Map<String,Integer>>> entries = methodCalls.entrySet().iterator();
        while (entries.hasNext()) {
            Map.Entry<String, Map<String,Integer>> entry = entries.next();
            System.out.println("Key = " + entry.getKey() + ", Value = \n");

            Map<String,Integer> list=entry.getValue();
            for(Map.Entry<String,Integer> s : list.entrySet())
                System.out.println(s);
        }
    }
    public Map<String, Map<String,Integer>> getMethodCalls() {
        return methodCalls;
    }
}