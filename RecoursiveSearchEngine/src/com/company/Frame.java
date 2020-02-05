package com.company;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;



public class Frame extends JFrame {

    public Frame() {

    Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize(); //object to get screensize functions

    this.setSize((int)screenSize.getWidth(),(int)screenSize.getHeight());


    this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);


    ImageIcon icon1 = new ImageIcon("test.png");        //creating icons for later use in scrollpanes
        JLabel label1 = new JLabel();                   //icon 1
        label1.setIcon(icon1);
        JScrollPane scrollPane1 = new JScrollPane(label1);
        scrollPane1.setBounds(0,0,1400,900);

    ImageIcon icon2 = new ImageIcon("pepedab.png");
        JLabel label2 = new JLabel();                   //icon 2
        label2.setIcon(icon2);
        JScrollPane scrollPane2 = new JScrollPane(label2);
        scrollPane2.setBounds(0,0,1400,900);



    ImageIcon icon3 = new ImageIcon("pepedab.png");
        JLabel label3 = new JLabel();           //icon 3
        label3.setIcon(icon3);
        JScrollPane scrollPane3 = new JScrollPane(label3);
        scrollPane3.setBounds(0,0,1400,900);


        JLayeredPane layeredPane = getLayeredPane();
        layeredPane.add(scrollPane1, 1);
        layeredPane.add(scrollPane2, 2);        //adding to a panel
        layeredPane.add(scrollPane3, 3);

        scrollPane1.setVisible(false);
        scrollPane2.setVisible(true);           //set true false to change which picture is displayed
        scrollPane3.setVisible(false);
        setVisible(true);



}





}