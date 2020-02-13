package com.company;

import java.util.*;

class MapSaver {
    private Map<String, List<String>> neighbourMap = new HashMap<>();
    private Map<Map<String, Integer>, List<String>> neighbourMap2 = new HashMap<>();
    private List<String> dataList;

    MapSaver(List<String> dataList) {
        this.dataList = dataList;
    }

    Map<Map<String, Integer>, List<String>> getMapWithAllData2() {
        OurReader reader = new OurReader(null);
        int lineCounterForFile=0;
        String temporaryClassName = "";
        List<String> temporaryImports = new LinkedList<>();
        for (String s : dataList) {
            if(!s.contains("File end")) {
                if (s.contains("class ") && !s.contains("(")) {
                    String line = s.strip();
                    int counterBegin = line.lastIndexOf("ss") + 2;
                    int counterEnd = line.lastIndexOf("{");
                    temporaryClassName = line.substring(counterBegin, counterEnd).strip();
                }

                if (s.contains("import ") && !s.contains("s.contains")) {
                    String line = s.strip();
                    int counterBegin = line.lastIndexOf(" ");
                    int counterEnd = line.lastIndexOf(";");
                    String imp = line.substring(counterBegin, counterEnd).strip();
                    temporaryImports.add(imp);
                }
                lineCounterForFile++;
            }else if(!s.contains("(")){
                Map<String,Integer> keyMap =new HashMap<>();
                keyMap.put(temporaryClassName,lineCounterForFile);
                neighbourMap2.put(keyMap, new LinkedList<>(temporaryImports));
                temporaryClassName="";
                temporaryImports.clear();
                lineCounterForFile=0;
            }
        }
        return neighbourMap2;
    }

    Map<String, List<String>> getMapWithAllData() {

        int documentsCounter = 0;
        List<String> classesNames = new LinkedList<>();
        List<String> imports = new LinkedList<>();

        if (dataList != null) {
            for (String s : dataList) {
                if (s.contains("class ") && !s.contains("(")) {
                    String line = s.strip();
                    int counterBegin = line.lastIndexOf("ss") + 2;
                    int counterEnd = line.lastIndexOf("{");
                    String className = line.substring(counterBegin, counterEnd).strip();
                    classesNames.add(className);
                }
            }
            for (String s : dataList) {
                if (s.contains("class ") && !s.contains("("))
                    imports.add("|");
                if (s.contains("import ") && !s.contains("s.contains")) {
                    String line = s.strip();
                    int counterBegin = line.lastIndexOf(" ");
                    int counterEnd = line.lastIndexOf(";");
                    String imp = line.substring(counterBegin, counterEnd).strip();
                    imports.add(imp);
                }
            }

            List<String> importsForSpecificMap = new LinkedList<>();
            for (String anImport : imports) {
                if (!anImport.equals("|")) {
                    importsForSpecificMap.add(anImport);
                } else {
                    this.neighbourMap.put(classesNames.get(documentsCounter), new LinkedList<>(importsForSpecificMap));
                    importsForSpecificMap.clear();
                    documentsCounter++;
                }
            }

        }
        return this.neighbourMap;
    }

}