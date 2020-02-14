package com.company;

import guru.nidi.graphviz.attribute.*;
import guru.nidi.graphviz.engine.*;
import guru.nidi.graphviz.model.*;

import java.io.*;
import java.util.*;

import static guru.nidi.graphviz.engine.Graphviz.*;
import static guru.nidi.graphviz.model.Factory.*;

public class MapImageGenerator {

    private String graphName;
    private Map<String, Integer> nodesWeights = null;
    private Map<String, Map<String,Integer>> neighbourMap = null;
    private MutableGraph graph;


    public MapImageGenerator(String name,Map<String,Map<String,Integer>> neighbourMap,Map<String,Integer> methodWeights) {
        this.neighbourMap = neighbourMap;
        this.graphName=name;
        this.nodesWeights =methodWeights;
    }

    void toPNG(boolean weighted) {
        graph= mutGraph(graphName).setDirected(true);

        for(String nodeName : neighbourMap.keySet()){
            Map<String,Integer> endNodes = neighbourMap.get(nodeName);

            MutableNode beg = mutNode(nodeName);

            for(String end : endNodes.keySet()){
                String value;
                if(weighted) {
                    value = endNodes.get(end).toString();
                } else {
                    value="";
                }
                //beg.addLink(Link.to(mutNode(a + "\n" + nodesWeights.get(a))).with(Label.of(value)));
                beg.addLink(Link
                        .to(mutNode(end))
                        .with(Label.of(value)));
            }
            graph.add(beg);
        }
        graphToPNG();
    }

    void weightlessToPNG() {
        toPNG(false);
    }

    void weightedToPNG() {
        toPNG(true);
    }

    private void graphToPNG(){
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

