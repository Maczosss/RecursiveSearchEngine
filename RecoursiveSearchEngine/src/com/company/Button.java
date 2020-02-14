package com.company;
import javax.swing.*;

public class Button extends JButton {

    public static int buttonAmount=0;
    private String buttonName;



    public Button(int scrollPanelWidth,String buttonName){


        this.buttonName=buttonName;
        setText("Historyjka "+buttonName);
        int buttonwidth=scrollPanelWidth/6;
        setBounds(scrollPanelWidth,buttonAmount*250+50,buttonwidth,(int)(buttonwidth/2.5));

        buttonAmount++;

    }


}

