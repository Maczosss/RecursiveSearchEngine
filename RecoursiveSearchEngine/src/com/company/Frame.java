package com.company;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class Frame extends JFrame {

        //JButton button1=new JButton("elo");
        //button1.setBounds(100,100,100,100);
        //add(button1);

        //setExtendedState(JFrame.MAXIMIZED_BOTH);
    public Frame() {
    this.setSize(1700, 1100);
    this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    JPanel panel = new JPanel();
   // setLayout(null);
    panel.setBackground(Color.cyan);
    ImageIcon icon = new ImageIcon("test.png");
    JLabel label1 = new JLabel();
    label1.setIcon(icon);
    panel.add(label1);
    this.getContentPane().add(panel);
    setVisible(true);


}





}