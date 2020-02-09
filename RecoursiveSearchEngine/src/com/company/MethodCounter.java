package com.company;

import java.util.*;

public class MethodCounter {
    private Map<String, List<String>> methodsInClassMap = new HashMap<>();
    private List<String> dataList;
    private Map<String, Integer> methodCounter = new HashMap<>();

    public MethodCounter(List<String> dataList) {
        this.dataList = dataList;
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
        this.methodsInClassMap = methodsInClassMap;
        System.out.println(methodsInClassMap);
        return methodsInClassMap;
    }

    public void show() {
        System.out.println(this.methodCounter);
    }

    public void getMethodsForClasses() {
        if(methodCounter.isEmpty()){
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
            for (String checkedMethod : methodsInClassMap.get(temp)) {

                String str = wholeClassesWithData.get(temp);

                String strFind = checkedMethod;
                int count = 0, fromIndex = 0;

                while ((fromIndex = str.indexOf(strFind, fromIndex)) != -1) {
                    count++;
                    fromIndex++;
                }
                methodCounter.put("Method: " + checkedMethod + " is called in class: " + temp, count);
            }
        }
        System.out.println(methodCounter);
    }
}