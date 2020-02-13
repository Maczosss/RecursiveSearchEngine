package com.company;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;



public class Frame extends JFrame implements ActionListener{

    private Button button1;
    private Button button2;
    private Button button3;
    private JScrollPane scrollPane1;
    private JScrollPane scrollPane2;
    private JScrollPane scrollPane3;

    public Frame(String []pngsPath) {

    Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize(); //object to get screensize functions

    int screenWidth =(int)screenSize.getWidth();
    int screenHeight=(int)screenSize.getHeight();       //screensize
    this.setSize(screenWidth,screenHeight);
    this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

    int scrollPanelHeight=screenHeight-70;          //panelsize
    int scrollPanelWidth=screenWidth-screenWidth/6;

            //icon 1
    ImageIcon icon1 = new ImageIcon(pngsPath[0]);        //creating icons for later use in scrollpanes
        JLabel label1 = new JLabel();
        label1.setIcon(icon1);
        scrollPane1 = new JScrollPane(label1);
        scrollPane1.setBounds(0,0,scrollPanelWidth,scrollPanelHeight);
        scrollPane1.getVerticalScrollBar().setUnitIncrement(16);
        scrollPane1.getHorizontalScrollBar().setUnitIncrement(16);
            //icon 2
    ImageIcon icon2 = new ImageIcon(pngsPath[1]);
        JLabel label2 = new JLabel();
        label2.setIcon(icon2);
        scrollPane2 = new JScrollPane(label2);
        scrollPane2.setBounds(0,0,scrollPanelWidth,scrollPanelHeight);
        scrollPane2.getVerticalScrollBar().setUnitIncrement(16);
        scrollPane1.getHorizontalScrollBar().setUnitIncrement(16);
          //icon 3
    ImageIcon icon3 = new ImageIcon(pngsPath[2]);
        JLabel label3 = new JLabel();
        label3.setIcon(icon3);
        scrollPane3 = new JScrollPane(label3);
        scrollPane3.setBounds(0,0,scrollPanelWidth,scrollPanelHeight);
        scrollPane3.getVerticalScrollBar().setUnitIncrement(16);
        scrollPane1.getHorizontalScrollBar().setUnitIncrement(16);


            //buttons
        button1 = new Button(scrollPanelWidth,"1");
        button2 = new Button(scrollPanelWidth,"2");
        button3 = new Button(scrollPanelWidth,"3");
            //buttons actionlisteners
        button1.addActionListener(this);
        button2.addActionListener(this);
        button3.addActionListener(this);

        JLayeredPane layeredPane = getLayeredPane();
        layeredPane.add(scrollPane1, 1);
        layeredPane.add(scrollPane2, 2);        //adding to a layeredPanel
        layeredPane.add(scrollPane3, 3);
        layeredPane.add(button1, 4);
        layeredPane.add(button2, 5);
        layeredPane.add(button3, 6);

        scrollPane1.setVisible(true);
        scrollPane2.setVisible(false);           //set true false to change which picture is displayed
        scrollPane3.setVisible(false);
        setVisible(true);

}


    @Override
    public void actionPerformed(ActionEvent e) {
        Object source = e.getSource();
        //displaying different panels
        if(source == button1)
        {
            scrollPane1.setVisible(true);
            scrollPane2.setVisible(false);
            scrollPane3.setVisible(false);

        }

        else if(source == button2)
        {
            scrollPane1.setVisible(false);
            scrollPane2.setVisible(true);
            scrollPane3.setVisible(false);

        }
        else if(source == button3)
        {
            scrollPane1.setVisible(false);
            scrollPane2.setVisible(false);
            scrollPane3.setVisible(true);

    }

    }


}