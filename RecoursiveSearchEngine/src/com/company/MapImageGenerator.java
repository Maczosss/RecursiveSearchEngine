package com.company;

import guru.nidi.graphviz.attribute.*;
import guru.nidi.graphviz.engine.*;
import guru.nidi.graphviz.model.*;

import java.io.*;
import java.util.*;

import static guru.nidi.graphviz.engine.Graphviz.*;
import static guru.nidi.graphviz.model.Factory.*;

public class MapImageGenerator {

    private Map<String, List<String>> neighbourMap = null;
    private String graphName;
    private Map<String, Integer> nodesWeights = null;
    private Map<String, Map<String,Integer>> neighbourMap2 = null;

    MapImageGenerator(String name, Map<String, List<String>> neighbourMap) {
        this.neighbourMap = neighbourMap;
        this.graphName=name;
    }
    MapImageGenerator(String name, Map<String, Map<String,Integer>> neighbourMap, Map<String, Integer> methodWeights) {
        this.neighbourMap2 = neighbourMap;
        this.graphName=name;
        this.nodesWeights =methodWeights;
    }



    void weightlessToPNG() {
        MutableGraph graph= mutGraph(graphName).setDirected(true);

        for(Map.Entry<String,List<String>> element : neighbourMap.entrySet()){
            String nodeName = element.getKey();
            List<String> nodes = element.getValue();
            MutableNode beg;
            //if(nodesWeights==null) {
                beg = mutNode(nodeName).add(Label.of(nodeName));
            for(String a : nodes){
                    beg.addLink(Link.to(mutNode(a)));
            }
            graph.add(beg);
        }

        String imageName = graph.name()+".png";
        try {
            fromGraph(graph)
                    .engine(Engine.FDP)
                    .fontAdjust(0.9)
                    .render(Format.PNG)
                    .toFile(new File(imageName));
            System.out.println(imageName + " generated successfully");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    void weightedToPNG() {
        MutableGraph graph= mutGraph(graphName).setDirected(true);

        for(Map.Entry<String,Map<String,Integer>> element : neighbourMap2.entrySet()){
            String nodeName = element.getKey();
            Map<String,Integer> nodes = element.getValue();
            MutableNode beg;
            //if(nodesWeights==null) {
            beg = mutNode(nodeName).add(Label.of(nodeName));
            // }else{
            //    beg = mutNode(nodeName).add(Label.of(nodeName + "\n" + nodesWeights.get(nodeName)));
            //}

            for(Map.Entry<String,Integer> a : nodes.entrySet()){
                String value = a.getValue().toString();
                //beg.addLink(Link.to(mutNode(a + "\n" + nodesWeights.get(a))).with(Label.of(value)));
                beg.addLink(Link.to(mutNode(a.getKey())).with(Label.of(value)));
            }
            graph.add(beg);
        }
        String imageName = graph.name()+".png";
        try {
            fromGraph(graph)
                    .engine(Engine.FDP)
                    .fontAdjust(0.9)
                    .render(Format.PNG)
                    .toFile(new File(imageName));
            System.out.println(imageName + " generated successfully");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

