using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Diagnostics;

namespace Repository;

public class ClassPrint
{
    private ClassCircle circle;
    private ClassBox box;
    private IEnumerable<ClassMaterial> materials;

    public ClassPrint(IEnumerable<ClassMaterial> materials)
    {
        this.materials = materials;
        ClassMaterial defaultMaterial = new ClassMaterial();
        circle = new ClassCircle(defaultMaterial);
        box = new ClassBox(defaultMaterial);
    }

    public void StartPrint()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text file (*.txt)|*.txt";
        if (saveFileDialog.ShowDialog() == true)
        {
            string formattedData = GatherData();
            PrintToFile(saveFileDialog.FileName, formattedData);
        }
    }

    private string GatherData()
    {
        StringBuilder formattedData = new StringBuilder();

        // Define column widths
        int[] columnWidths = new int[] { 25, 16, 16, 20, 20, 25, 30, 15, 15, 15, 20, 20 };

        // Add headers with padding for alignment
        string headers = "Materiale".PadRight(columnWidths[0]) +
                         "Massefylde(m³)".PadRight(columnWidths[1]) +
                         "Tykkelse(mm)".PadRight(columnWidths[2]) +
                         "Cylinder Højde".PadRight(columnWidths[3]) +
                         "Cylinder Radius".PadRight(columnWidths[4]) +
                         "Cylinder Ydre Volume".PadRight(columnWidths[5]) +
                         "Cylinder Bæreevne".PadRight(columnWidths[6]) +
                         "Box Højde".PadRight(columnWidths[7]) +
                         "Box Bredde".PadRight(columnWidths[8]) +
                         "Box Længde".PadRight(columnWidths[9]) +
                         "Box Ydre Volumen".PadRight(columnWidths[10]) +
                         "Box Bæreevne".PadRight(columnWidths[11]);

        formattedData.AppendLine($"{headers}\n");

        foreach (var material in materials)
        {
            circle.SelectedMaterial = material;
            box.SelectedMaterial = material;

            // Add data with padding for each column
            string dataLine = material.Name.PadRight(columnWidths[0]) +
                              material.Density.ToString().PadRight(columnWidths[1]) +
                              material.Thickness.ToString().PadRight(columnWidths[2]) +
                              circle.Height.PadRight(columnWidths[3]) +
                              circle.Radius.PadRight(columnWidths[4]) +
                              circle.CalculateOuterDimensions.PadRight(columnWidths[5]) +
                              circle.CalculateBuoyancyData.PadRight(columnWidths[6]) +
                              box.Height.PadRight(columnWidths[7]) +
                              box.Width.PadRight(columnWidths[8]) +
                              box.Depth.PadRight(columnWidths[9]) +
                              box.CalculateOuterDimensions.PadRight(columnWidths[10]) +
                              box.CalculateBuoyancyData.PadRight(columnWidths[11]);

            formattedData.AppendLine(dataLine);
        }

        return formattedData.ToString();
    }


    private void PrintToFile(string filePath, string formattedData)
    {
        File.WriteAllText(filePath, formattedData);

        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }
}
