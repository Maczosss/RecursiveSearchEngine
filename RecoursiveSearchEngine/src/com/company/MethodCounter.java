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

                if(!declarationList.contains(temp)) {
                    declarationList.add(temp);
                }
                //znajduje swoje deklaracje funckji i tworze ich liste
            }
        }

        System.out.println(declarationList);
        System.out.println(declarationList.size());



        for (String method: declarationList) {
            int counter = 1;
            boolean flag = false;
            Map<String,Integer> localMethodsCalls = new HashMap<>();
            for (String line: dataList) {
                if(line.contains("//"))
                    continue;
                if(flag){
                    if (line.contains("{"))
                        counter++;
                    if (line.contains("}"))
                        counter--;
                    for (String methodCall: declarationList){
                        if(!line.contains(methodCall)){
                            continue;
                        }
                        if(!localMethodsCalls.containsKey(methodCall+")")) {
                            localMethodsCalls.put(methodCall+")", 1);
                        } else {
                            Integer temp = localMethodsCalls.get(methodCall+")");
                            temp++;
                            localMethodsCalls.put(methodCall+")", temp);
                        }
                    }
                    if(counter==0){
                        break;
                    }
                } else if(line.contains(method) && line.contains("{")){
                    flag=true;
                }

            }
            methodCalls.put(method+")",localMethodsCalls);
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