package com.company;

import guru.nidi.graphviz.attribute.*;
import guru.nidi.graphviz.engine.*;
import guru.nidi.graphviz.model.*;

import java.io.*;
import java.util.*;

import static guru.nidi.graphviz.engine.Graphviz.*;
import static guru.nidi.graphviz.model.Factory.*;

public class MapImageGenerator {

    private Map<String, List<String>> neighbourMap;
    private String graphName = "test";

    MapImageGenerator(Map<String, List<String>> neighbourMap) {
        this.neighbourMap = neighbourMap;
    }


    void toPNG() {
        MutableGraph graph= mutGraph(graphName).setDirected(true);

        for(Map.Entry<String,List<String>> element : neighbourMap.entrySet()){
            String nodeName = element.getKey();
            List<String> nodes = element.getValue();
            String nodeSize = " ";

            MutableNode beg = mutNode(nodeName).add(Label.of(nodeName + "\n" + nodeSize));

            for(String a : nodes){
                String value = "1"; //a;
                beg.addLink(Link.to(mutNode(a)).with(Label.of(value)));
            }
            graph.add(beg);
        }


        //String imageName = graph.name()+"_"+System.nanoTime()+".png";
        String imageName = graph.name()+".png";
        try {
            fromGraph(graph)
                    .engine(Engine.FDP)
                    .fontAdjust(0.9)
                    //.height(400)
                    //.width(400)
                    .render(Format.PNG)
                    .toFile(new File(imageName));
            System.out.println(imageName + " generated successfully");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

