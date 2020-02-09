package Test;

import com.company.MethodCounter;
import com.company.Reader;
import org.junit.Test;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import static org.junit.Assert.*;

public class MethodCounterTest {

    @Test
    public void getMethodMapTest() {
        String fileName = "C:\\Users\\macie\\OneDrive\\Desktop\\Gir repository\\RecursiveSearchEngine\\RecoursiveSearchEngine\\src\\com\\company";
        Reader reader = new Reader(fileName);
        MethodCounter methodCounter = new MethodCounter(reader.getTextFromFiles());
        List<String> result = new LinkedList<>();
        List<String> finalResult = new LinkedList<>();
        try (Stream<Path> walk = Files.walk(Paths.get(fileName))) {

            result = walk.filter(Files::isRegularFile)
                    .map(Path::toString).collect(Collectors.toList());

            result.forEach(System.out::println);

        } catch (IOException e) {
            e.printStackTrace();
        }
        for(String temp: result){
            String className ="";
            int beginning = temp.lastIndexOf("\\");
            int end = temp.lastIndexOf(".");
            className = temp.substring(beginning+1,end);
            finalResult.add(className);
        }
        for(String temp : finalResult){
            assertTrue(methodCounter.getMethodMap().containsKey(temp));
        }

    }

    @Test
    public void show() {
        String fileName = "C:\\Users\\macie\\OneDrive\\Desktop\\Gir repository\\RecursiveSearchEngine\\RecoursiveSearchEngine\\src\\com\\company";
        Reader reader = new Reader(fileName);
        MethodCounter methodCounter = new MethodCounter(reader.getTextFromFiles());
    }

    @Test
    public void getMethodsForClasses() {
    }
}