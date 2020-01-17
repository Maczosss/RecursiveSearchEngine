package com.company;

import java.util.*;

public class MethodCounter {
    //nazwa metody, nazwa klasy  , nazwa metody przeszukiwanej i ilosc wywolan metody
    private Map<Map<String, String>, Map<String, Integer>> methodCounter = new HashMap<>();

    private Map<String, List<String>> neighbourMap = new HashMap<>();
    private Map<Map<String, Integer>, List<String>> neighbourMap2 = new HashMap<>();

    private List<String> dataList;

    MethodCounter(List<String> dataList) {
        this.dataList = dataList;
    }


//    Map<Map<String, String>, Map<String, Integer>> methodCounterInFiles() {
    Map<String, List<String>> getMethodMap() {
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
                &&!s.contains("for")&&!s.contains("if")&&!s.contains("while")&&!s.contains("catch")) {
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
        return methodsInClassMap;
    }
}


