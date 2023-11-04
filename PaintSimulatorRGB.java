package paintsimulatorrgb;
import java.util.*;
import java.io.*;

public class PaintSimulatorRGB {
    public static void main(String[] args) {
        String fileName = "D:/JAVAFILES/RGBOutput.txt";
        String outputPath = "D:/JAVAFILES/RGBCode.txt";
        int count = 1;
        try {
            File fileIn = new File(fileName);
            Scanner fileReader = new Scanner(fileIn);
            FileWriter fileWriter = new FileWriter(outputPath);
            String firstIfStatement = fileReader.nextLine();
            String[] getFirstInfo = firstIfStatement.split(":");
            System.out.println("Test");
            String colorName = getFirstInfo[0].substring(0,getFirstInfo[0].length()-4);
            System.out.println("SecondTest");
            String colorNumbers = getFirstInfo[1].trim();
            String[] colors = colorNumbers.split(",");
            int red = Integer.parseInt(colors[0].substring(1,colors[0].length()));
            int green = Integer.parseInt(colors[1].substring(1,colors[1].length()));
            int blue = Integer.parseInt(colors[2].substring(1,colors[2].length()));
            fileWriter.write("if (count == " + count + ")\n\t{\n\t\tcolorName = \"" + colorName + "\";\n\t\torderCreated = true;\n\t\tred = " + red + ";\n\t\tgreen = " + green + ";\n\t\tblue = " + blue + ";\n\t}\n");
            count++;
            while(fileReader.hasNextLine())
            {
                String IfStatement = fileReader.nextLine();
                System.out.println(IfStatement);
                String[] getInfo = IfStatement.split(":");
                String getColorName = getInfo[0].substring(0,getInfo[0].length()-4);
                System.out.println(getColorName);
                String getColorNumbers = getInfo[1];
                String[] getColors = getColorNumbers.split(",");
                int redVal = Integer.parseInt(getColors[0].substring(1,getColors[0].length()));
                String greenValString = getColors[1].substring(1,getColors[1].length());
                int greenVal = Integer.parseInt(greenValString);
                int blueVal = Integer.parseInt(getColors[2].substring(1,getColors[2].length()));
                fileWriter.write("else if (count == " + count + ")\n\t{\n\t\tcolorName = \"" + getColorName + "\";\n\t\torderCreated = true;\n\t\tred = " + redVal + ";\n\t\tgreen = " + greenVal + ";\n\t\tblue = " + blueVal + ";\n\t}\n");
                count++;
            }
            fileWriter.close();
            
            
            
//            ArrayList<String> findDups = new ArrayList<>();
//            HashSet<String> duplicateNames = new HashSet<>();
//
//            File fileIn = new File(fileName);
//            Scanner fileReader = new Scanner(fileIn);
//
//            while (fileReader.hasNextLine()) {
//                String fileOut = fileReader.nextLine();
//                String[] getName = fileOut.split(":");
//                String colorName = getName[0].trim();
//                if (findDups.contains(colorName)) {
//                    duplicateNames.add(colorName);
//                } else {
//                    findDups.add(colorName);
//                }
//            }
//
//            System.out.println("Duplicate Names:");
//            for (String name : duplicateNames) {
//                System.out.println(name);
//            }
        } catch (Exception ex) {
            System.out.println("An error occurred: " + ex);
        }
    }
}
