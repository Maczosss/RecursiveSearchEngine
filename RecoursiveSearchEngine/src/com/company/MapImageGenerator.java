package com.company;

import guru.nidi.graphviz.attribute.*;
import guru.nidi.graphviz.engine.*;
import guru.nidi.graphviz.model.*;

import javax.lang.model.type.NullType;
import java.io.*;
import java.util.*;

import static guru.nidi.graphviz.engine.Graphviz.*;
import static guru.nidi.graphviz.model.Factory.*;

public class MapImageGenerator {

    private Map<String, List<String>> neighbourMap;
    private String graphName;
    private Map<String, Integer> methodWeights = null;


    MapImageGenerator(String name, Map<String, List<String>> neighbourMap) {
        this.neighbourMap = neighbourMap;
        this.graphName=name;
    }
    MapImageGenerator(String name, Map<String, List<String>> neighbourMap, Map<String, Integer> methodWeights) {
        this.neighbourMap = neighbourMap;
        this.graphName=name;
        this.methodWeights=methodWeights;
    }


    void toPNG() {
        MutableGraph graph= mutGraph(graphName).setDirected(true);

        for(Map.Entry<String,List<String>> element : neighbourMap.entrySet()){
            String nodeName = element.getKey();
            List<String> nodes = element.getValue();
            MutableNode beg;
            //if(methodWeights==null) {
                beg = mutNode(nodeName).add(Label.of(nodeName));
           // }else{
            //    beg = mutNode(nodeName).add(Label.of(nodeName + "\n" + methodWeights.get(nodeName)));
            //}

            for(String a : nodes){
                String value;
                if(methodWeights==null) {
                    value = "1"; //a;
                    beg.addLink(Link.to(mutNode(a)).with(Label.of(value)));
                }else{
                    value = methodWeights.get(a).toString();
                    //beg.addLink(Link.to(mutNode(a + "\n" + methodWeights.get(a))).with(Label.of(value)));
                    beg.addLink(Link.to(mutNode(a)).with(Label.of(value)));
                }

            }
            graph.add(beg);
        }


        //String imageName = graph.name()+"_"+System.nanoTime()+".png";
        String imageName = graph.name()+".png";
        try {
            fromGraph(graph)
                    .engine(Engine.FDP)
                    .fontAdjust(0.9)
                    //.height(4000)
                    //.width(4000)
                    .render(Format.PNG)
                    .toFile(new File(imageName));
            System.out.println(imageName + " generated successfully");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

